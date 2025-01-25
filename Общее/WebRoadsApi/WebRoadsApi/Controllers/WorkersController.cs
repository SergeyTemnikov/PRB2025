using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using WebRoadsApi.ClassHelpers;
using WebRoadsApi.Data;
using WebRoadsApi.Models;

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
                return NotFound(ex.Message);
            }
        }

        [Route("/Workers/DeleteWorker/{id}")]
        [HttpDelete]
        public IActionResult DeleteWorker(int id)
        {
            var worker = _db.Workers.FirstOrDefault(x => x.IdWorker == id);
            if (worker == default)
            {
                return NotFound("Такого работника не существует.");
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
                return StatusCode(500, "Ошибка при увольнении сотрудника: " + ex.Message);
            }
        }

        [Route("/Workers/ChangeWorker/{id}")]
        [HttpPut]
        public IActionResult UpdateWorker(int id, [FromBody] Worker worker)
        {
            if (worker == null)
            {
                return BadRequest("Данные работника не предоставлены.");
            }

            if (id != worker.IdWorker)
            {
                return BadRequest("Идентификатор в URL и в теле запроса не совпадают.");
            }

            var originWorker = _db.Workers.FirstOrDefault(x => x.IdWorker == id);
            if (originWorker == null)
            {
                return NotFound("Работник не найден.");
            }

            _db.Workers.Update(worker);

            try
            {
                _db.SaveChanges();
                return Ok("Данные работника успешно обновлены.");
            }
            catch (Exception ex)
            {               
                return StatusCode(500, "Произошла ошибка при обновлении данных: " + ex.Message);
            }
        }

        [Route("/Workers/GetWorkersCalendar/{id}")]
        [HttpGet]
        public IActionResult GetWorker(int id, [FromBody] DateState state)
        {
            var worker = _db.Workers.FirstOrDefault(x => x.IdWorker == id);
            if (worker == null)
            {
                return NotFound("Пользователь не найден!");
            }

            var trainings = new List<TrainingCalendar>();
            var misses = new List<MissCalendar>();
            var holidays = new List<HolidayCalendar>();

            switch(state)
            {
                case DateState.Past:
                    trainings = _db.TrainingCalendars.Where(x => x.IdWorker == id && x.EndDateTime.Date < DateTime.Today).ToList();
                    misses = _db.MissCalendars.Where(x => x.IdWorker == id && Convert.ToDateTime(x.MissDate) < DateTime.Today).ToList();
                    holidays = _db.HolidayCalendars.Where(x => x.IdWorker == id && Convert.ToDateTime(x.EndDate) < DateTime.Today).ToList();
                    break;
                case DateState.Present:
                    trainings = _db.TrainingCalendars.Where(x => x.IdWorker == id && x.StartDateTime.Date < DateTime.Today && x.EndDateTime.Date > DateTime.Today).ToList();
                    misses = _db.MissCalendars.Where(x => x.IdWorker == id && Convert.ToDateTime(x.MissDate) == DateTime.Today).ToList();
                    holidays = _db.HolidayCalendars.Where(x => x.IdWorker == id && Convert.ToDateTime(x.StartDate) < DateTime.Today && Convert.ToDateTime(x.EndDate) > DateTime.Today).ToList();
                    break;
                case DateState.Future:
                    trainings = _db.TrainingCalendars.Where(x => x.IdWorker == id && x.StartDateTime.Date > DateTime.Today).ToList();
                    misses = _db.MissCalendars.Where(x => x.IdWorker == id && Convert.ToDateTime(x.MissDate) > DateTime.Today).ToList();
                    holidays = _db.HolidayCalendars.Where(x => x.IdWorker == id && Convert.ToDateTime(x.StartDate) > DateTime.Today).ToList();
                    break;
            }

            ObservableCollection<CalendarNode> nodes = new ObservableCollection<CalendarNode>();

            ObservableCollection<CalendarNode> trainigNodes = new ObservableCollection<CalendarNode>();
            foreach (var node in trainings)
            {
                trainigNodes.Add(new CalendarNode()
                {
                    Name = node.StartDateTime.ToString() + " - " + node.EndDateTime.ToString(),
                });
            }

            ObservableCollection<CalendarNode> missNodes = new ObservableCollection<CalendarNode>();
            foreach (var node in misses)
            {
                missNodes.Add(new CalendarNode()
                {
                    Name = node.MissDate.ToString(),
                });
            }

            ObservableCollection<CalendarNode> holidaysNodes = new ObservableCollection<CalendarNode>();
            foreach (var node in holidays)
            {
                holidaysNodes.Add(new CalendarNode()
                {
                    Name = node.StartDate.ToString() + " - " + node.EndDate.ToString(),
                });
            }

            nodes.Add(new CalendarNode()
            {
                Name = "Обучения",
                Days = trainigNodes
            });
            nodes.Add(new CalendarNode()
            {
                Name = "Отгулы",
                Days = missNodes
            });
            nodes.Add(new CalendarNode()
            {
                Name = "Отпуска",
                Days = holidaysNodes
            });

            return Ok(JsonConvert.SerializeObject(nodes, settings));

        }


        public enum DateState
        {
            Past,
            Present,
            Future
        }
    }
}
