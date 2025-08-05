namespace PeliculasAPI.Entidades
{
    public class PeliculaActor
    {
        public int ActorId { get; set; }
        public int PeliculaId { get; set; }
        public required string Personaje { get; set; }
        public int Orden { get; set; }
        public Actor Actor { get; set; } = null!;
        public Pelicula Pelicula { get; set; } = null!;
        // Propiedad para indicar si el actor es el protagonista de la película
        //public bool EsProtagonista { get; set; } = false;
    }
}
