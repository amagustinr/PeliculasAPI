using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Unicode(false)]
        public string? Foto { get; set; }
    }
}
