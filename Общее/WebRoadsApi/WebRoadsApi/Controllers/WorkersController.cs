using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebRoadsApi.Data;

namespace WebRoadsApi.Controllers
{
    public class WorkersController : Controller
    {
        PrbContext _db = new();

        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Workers/GetWorkers")]
        [HttpGet]
        public IActionResult GetWorkers()
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.Workers.ToList(), settings));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/Workers/DeleteWorker/{id}")]
        [HttpDelete]
        public IActionResult DeleteWorker(int id)
        {
            var worker = _db.Workers.FirstOrDefault(x => x.IdWorker == id);
            if (worker == default)
            {
                return BadRequest("Такого работника не существует.");
            }

            var missDays = _db.MissCalendars.Where(x => x.IdWorker == id && Convert.ToDateTime(x.MissDate) > DateTime.Today).ToList();
            var holidays = _db.HolidayCalendars.Where(x => x.IdWorker == id && Convert.ToDateTime(x.StartDate) > DateTime.Today).ToList();

            if (missDays != null)
            {
                foreach (var m in missDays)
                {
                    _db.MissCalendars.Remove(m);
                }
            }
            if(holidays != null)
            {
                foreach(var h in holidays)
                {
                    _db.HolidayCalendars.Remove(h);
                }
            }

            worker.IsWorking = false;
            worker.LastWorkDay = DateOnly.FromDateTime(DateTime.Today);

            try
            {
                _db.SaveChanges();
                return Ok("Сотрудник уволен!");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при увольнении сотрудника: " + ex.Message);
            }
        }

    }
}
