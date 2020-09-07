using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;
using PeliculasAPI.Helpers;
using PeliculasAPI.Services;

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public ActoresController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos): base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            //var queryable = context.Actores.AsQueryable();
            //await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.CantidadRegistroPorPagina);
            //var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();

            //var mapeo = mapper.Map<List<ActorDTO>>(entidades);

            //return mapeo;
            return await Get<Actor, ActorDTO>(paginacionDTO);
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            var actorDTO = mapper.Map<ActorDTO>(actor);

            return actorDTO;
        }

        //return await Get<Actor, ActorDTO>(id);

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var entidad = mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    entidad.Foto = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor,
                        actorCreacionDTO.Foto.ContentType);
                }
            }

            context.Actores.Add(entidad);
            await context.SaveChangesAsync();
            var dto = mapper.Map<ActorDTO>(entidad);

            return new CreatedAtRouteResult("obtenerACtor", new { id = entidad.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            //var entidad = mapper.Map<Actor>(actorCreacionDTO);
            //entidad.Id = id;
            //context.Entry(entidad).State = EntityState.Modified;

            var actordb = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if(actordb == null)
            {
                return NotFound();
            }

            //mapeara solo los campos que tienen cambios
            actordb = mapper.Map(actorCreacionDTO, actordb);

            if (actorCreacionDTO.Foto != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await actorCreacionDTO.Foto.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(actorCreacionDTO.Foto.FileName);
                    actordb.Foto = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor,
                        actordb.Foto,actorCreacionDTO.Foto.ContentType);
                }
            }

            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            //if (patchDocument == null)
            //{
            //    return BadRequest();
            //}

            //var entidadDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            //if (entidadDB == null)
            //{
            //    return NotFound();
            //}

            //var entidadDTO = mapper.Map<ActorPatchDTO>(entidadDB);

            //patchDocument.ApplyTo(entidadDTO, ModelState);

            //var esvalido = TryValidateModel(entidadDTO);

            //if (!esvalido)
            //{
            //    return BadRequest(ModelState);
            //}

            //mapper.Map(entidadDTO, entidadDB);

            //await context.SaveChangesAsync();

            //return NoContent();
            return await Patch<Actor, ActorPatchDTO>(id, patchDocument);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            //if (actor == null)
            //    return NotFound();

            //context.Actores.Remove(actor);
            //await context.SaveChangesAsync();

            //return NoContent();

            return await Delete<Actor>(id);
        }
    }
}