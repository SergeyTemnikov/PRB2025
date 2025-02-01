using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebRoadsApi.Data;

namespace WebRoadsApi.Controllers
{
    public class CabinetController : Controller
    {
        PrbContext _db = new PrbContext();

        JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Cabinet/GetCabinets")]
        [HttpGet]
        public IActionResult GetCabinets()
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.Cabinets.ToList(), _settings));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        } 
    }
}
