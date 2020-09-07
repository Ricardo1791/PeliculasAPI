using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : CustomBaseController
    {
        public readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext context,
            IMapper mapper): base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            //var generos = await context.Generos.ToListAsync();
            //var generoDTO = mapper.Map<List<GeneroDTO>>(generos);

            //return generoDTO;
            return await Get<Genero, GeneroDTO>();
        }

        [HttpGet("{id}", Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            //var entidad = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            //if (entidad == null)
            //{
            //    return NotFound();
            //}

            //var dto = mapper.Map<GeneroDTO>(entidad);

            //return dto;
            return await Get<Genero, GeneroDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            //var entidad = mapper.Map<Genero>(generoCreacionDTO);

            //context.Generos.Add(entidad);

            //await context.SaveChangesAsync();

            //var generoDTO = mapper.Map<GeneroDTO>(entidad);

            //return new CreatedAtRouteResult("obtenerGenero", new { id = generoDTO.Id }, generoDTO);

            return await Post<GeneroCreacionDTO, Genero, GeneroDTO>(generoCreacionDTO, "obtenerGenero");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            //var entidad = mapper.Map<Genero>(generoCreacionDTO);
            //entidad.Id = id;
            //context.Entry(entidad).State = EntityState.Modified;

            //await context.SaveChangesAsync();

            //return NoContent();

            return await Put<GeneroCreacionDTO, Genero>(id, generoCreacionDTO);
        }

        [HttpDelete]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
        public async Task<ActionResult> Delete (int id)
        {
            //var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            //if (genero == null)
            //{
            //    return NotFound();
            //}

            //context.Generos.Remove(genero);
            //await context.SaveChangesAsync();

            //return NoContent();
            return await Delete<Genero>(id);
        }

    }
}