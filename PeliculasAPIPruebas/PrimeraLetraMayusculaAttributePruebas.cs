using PeliculasAPI.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPIPruebas
{
    [TestClass]
    public sealed class PrimeraLetraMayusculaAttributePruebas
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("      ")]
        [DataRow(null)]
        public void IsValid_DebeRetornarExito_SiValorEsVacio(string valor)
        {
            // Preparar
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var validationContext = new ValidationContext(new object{ });

            // Probar
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, validationContext);

            // Verificar
            Assert.AreEqual(expected: ValidationResult.Success, actual: resultado);
        }

        [TestMethod]
        [DataRow("Felipe")]
        public void IsValid_DebeRetornarExito_SiLaPrimeraLetraEsMayuscula(string valor)
        {
            // Preparar
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var validationContext = new ValidationContext(new object { });

            // Probar
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, validationContext);

            // Verificar
            Assert.AreEqual(expected: ValidationResult.Success, actual: resultado);
        }

        [TestMethod]
        [DataRow("felipe")]
        public void IsValid_DebeRetornarExito_SiLaPrimeraLetraNoEsMayuscula(string valor)
        {
            // Preparar
            var primeraLetraMayuscula = new PrimeraLetraMayusculaAttribute();
            var validationContext = new ValidationContext(new object { });

            // Probar
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, validationContext);

            // Verificar
            Assert.AreEqual(expected: "La primera letra debe ser mayúscula", actual: resultado!.ErrorMessage);
        }
    }
}
