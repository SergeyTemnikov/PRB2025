using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using WebRoadsApi.Data;

namespace WebRoadsApi.Controllers
{
    public class DepartamentController : Controller
    {
        RoadsContext _db = new();
        JsonSerializerSettings settings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Departament/GetDepartaments")]
        [HttpGet]
        public IActionResult GetDepartaments()
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.Departaments.ToList(), settings));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/Departament/GetSubDepartaments")]
        [HttpGet]
        public IActionResult GetSubDepartaments()
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.SubDepartaments.ToList(), settings));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
