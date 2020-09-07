using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Validaciones
{
    public class PesoArchivoValidacion: ValidationAttribute
    {
        private readonly int pesoMaxEnMb;

        public PesoArchivoValidacion(int PesoMaxEnMb)
        {
            pesoMaxEnMb = PesoMaxEnMb;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var formFile = (IFormFile)value;

            if(formFile == null)
            {
                return ValidationResult.Success;
            }

            if(formFile.Length > pesoMaxEnMb * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {pesoMaxEnMb}MB");
            }

            return ValidationResult.Success;
        }

    }
}
