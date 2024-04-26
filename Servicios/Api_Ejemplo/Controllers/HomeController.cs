using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Servicio_API.Areas
{
    [Area("api")]
    [Route("TRAMITESDGAR/api/[controller]")]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    public class HomeController : Controller
    {

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            resultApi result = new resultApi();
            result.id = 0;
            result.mensaje = "Api funcionando";
            return Ok(result);
        }

        public class resultApi
        {
            public int id { get; set; }
            public string mensaje { get; set; }
        }
    }
}
