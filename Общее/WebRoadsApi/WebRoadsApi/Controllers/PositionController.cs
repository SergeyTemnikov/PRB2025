using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebRoadsApi.Data;

namespace WebRoadsApi.Controllers
{
    public class PositionController : Controller
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

        [Route("/Position/GetPositions")]
        [HttpGet]
        public IActionResult GetPositions() 
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.Positions.ToList(), _settings));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
