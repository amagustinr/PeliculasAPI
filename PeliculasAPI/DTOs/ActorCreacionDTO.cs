using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.DTOs
{
    public class ActorCreacionDTO
    {
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Unicode(false)]
        public IFormFile? Foto { get; set; }
    }
}
