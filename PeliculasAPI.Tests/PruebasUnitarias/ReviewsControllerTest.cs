using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Tests.PruebasUnitarias
{
    [TestClass]
    public class ReviewsControllerTest: BasePruebas
    {
        [TestMethod]
        public async Task UsuarioNoPuedeCrear2ReviewsParaLaMismaPelicula()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            crearPeliculas(nombreBD);

            var peliculaId = await contexto.Peliculas.Select(x => x.Id).FirstAsync();
            var review1 = new Review() { PeliculaId = peliculaId, UsuarioId = usuarioPorDefectoId, Puntuacion = 5 };

            contexto.Add(review1);
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var controller = new ReviewController(contexto2, mapper);
            controller.ControllerContext = ConstruirControllerContext();

            var reviewCreacionDTO = new ReviewCreacionDTO() { Puntuacion = 4 };
            var respuesta = await controller.Post(peliculaId, reviewCreacionDTO);

            var valor = respuesta as IStatusCodeActionResult;

            Assert.AreEqual(400, valor.StatusCode);

        }

        [TestMethod]
        public async Task CrearReview()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            crearPeliculas(nombreBD);

            var peliculaId = await contexto.Peliculas.Select(x => x.Id).FirstAsync();
            var contexto2 = ConstruirContext(nombreBD);

            var mapper = ConfigurarAutoMapper();
            var controller = new ReviewController(contexto2, mapper);
            controller.ControllerContext = ConstruirControllerContext();

            var reviewcreacionDTO = new ReviewCreacionDTO() { Puntuacion = 5 };

            var respuesta = await controller.Post(peliculaId, reviewcreacionDTO);

            var valor = respuesta as NoContentResult;

            Assert.IsNotNull(valor);

            var contexto3 = ConstruirContext(nombreBD);
            var reviewDB = contexto3.Reviews.First();
            Assert.AreEqual(usuarioPorDefectoId, reviewDB.UsuarioId);

        }

        private void crearPeliculas(string nombreBD)
        {
            var contexto = ConstruirContext(nombreBD);
            contexto.Peliculas.Add(new Entities.Pelicula() { Titulo = "Pelicula 1" });
            contexto.SaveChanges();
        }


    }
}
