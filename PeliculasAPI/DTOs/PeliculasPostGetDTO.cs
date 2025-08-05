namespace PeliculasAPI.DTOs
{
    public class PeliculasPostGetDTO
    {
        public List<GeneroDTO> Generos { get; set; } = new List<GeneroDTO>();
        public List<CineDTO> Cines { get; set; } = new List<CineDTO>();
        //public List<ActorDTO> Actores { get; set; } = new List<ActorDTO>();
        //public PeliculaCreacionDTO Pelicula { get; set; } = new PeliculaCreacionDTO();
    }
}
