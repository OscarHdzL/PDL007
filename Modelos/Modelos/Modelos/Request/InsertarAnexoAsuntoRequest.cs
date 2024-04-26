using Microsoft.AspNetCore.Http;

namespace Modelos.Modelos.Request
{
    public class InsertarAnexoAsuntoRequest
    {
        public int id_asunto { get; set; }
        public string nombre_anexo { get; set; }
        public IFormFile anexo { get; set; }
        public string extension { get; set; }
        public int id_tramite { get; set; }
    }
}
