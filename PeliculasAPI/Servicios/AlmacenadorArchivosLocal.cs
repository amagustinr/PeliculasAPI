
namespace PeliculasAPI.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Almacenar(string contenedor, IFormFile archivo)
        {
            var extension = Path.GetExtension(archivo.FileName);
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, contenedor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            using (var ms = new MemoryStream())
            {
                await archivo.CopyToAsync(ms);
                var contenido = ms.ToArray();
                await File.WriteAllBytesAsync(ruta, contenido);
            }

            var request = httpContextAccessor.HttpContext!.Request!;

            var url = $"{request.Scheme}://{request.Host}";
            var urlArcchivo = Path.Combine(url, contenedor, nombreArchivo).Replace("\\", "/");

            return urlArcchivo;
        }

        public Task Borrar(string? ruta, string contenedor)
        {
            if (string.IsNullOrWhiteSpace(ruta))
            {
                return Task.CompletedTask;
            }

            var nombreArchivo = Path.GetFileName(ruta);
            var directorioAchivo = Path.Combine(env.WebRootPath, contenedor);

            if (File.Exists(directorioAchivo)) { 
                File.Delete(directorioAchivo);
            }

            return Task.CompletedTask;
        }
    }
}
