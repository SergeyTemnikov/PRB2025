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

        [Route("/Workers/GetWorker/{id}")]
        [HttpGet]
        public IActionResult GetWorkerById(int id)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.Workers.Where(x => x.IdWorker == id).FirstOrDefault(), settings));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("/Workers/GetWorkersPrivateInfo")]
        [HttpGet]
        public IActionResult GetWorkersPrivateInfo()
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.WorkerPrivateInfos.ToList(), settings));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("/Workers/GetWorkersPrivateInfo/{id}")]
        [HttpGet]
        public IActionResult GetWorkerPrivateInfoById(int id)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.WorkerPrivateInfos.Where(x => x.IdWorker == id).FirstOrDefault(), settings));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("/Workers/GetWorkersFromDepartament/{id}")]
        [HttpGet]
        public IActionResult GetWorkersFromDepartament(int id)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_db.Workers.Where(x => x.IdDepartament == id).ToList(), settings));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Route("/Workers/GetWorkersFromDepartamentAndChilds/{id}")]
        [HttpGet]
        public IActionResult GetWorkersFromDepartamentAndChilds(int id)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(GetListWorkers(id), settings));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        private List<Worker> GetListWorkers(int id, List<Worker> workers = null)
        {
            if (workers == null)
            {
                workers = new List<Worker>();
            }

            var workersFromDep = _db.Workers.Where(x => x.IdDepartament == id).ToList();
            workers.AddRange(workersFromDep);

            var departaments = _db.Departaments.Where(x => x.IdParentDepartament == id).ToList();

            foreach (var department in departaments)
            {
                var subWorkers = GetListWorkers(department.IdDepartament); 
                workers.AddRange(subWorkers);
            }

            return workers;
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

            var missDays = _db.MissCalendars.Where(x => x.IdWorker == id && x.MissDate > DateTime.Today).ToList();
            var holidays = _db.HolidayCalendars.Where(x => x.IdWorker == id && x.StartDate > DateTime.Today).ToList();

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
            worker.LastWorkDay = DateTime.Today;

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
        public IActionResult GetWorker(int id)
        {
            var worker = _db.Workers.FirstOrDefault(x => x.IdWorker == id);
            if (worker == null)
            {
                return NotFound("Пользователь не найден!");
            }

            var trainings = _db.TrainingCalendars.Where(x => x.IdWorker == id).ToList();
            var misses = _db.MissCalendars.Where(x => x.IdWorker == id).ToList();
            var holidays = _db.HolidayCalendars.Where(x => x.IdWorker == id).ToList();

            ObservableCollection<CalendarNode> nodes = new ObservableCollection<CalendarNode>();

            ObservableCollection<CalendarDays> trainigNodes = new ObservableCollection<CalendarDays>();
            foreach (var node in trainings)
            {
                trainigNodes.Add(new CalendarDays()
                {
                    DateStart = node.StartDateTime,
                    DateEnd = node.EndDateTime,
                });
            }

            ObservableCollection<CalendarDays> missNodes = new ObservableCollection<CalendarDays>();
            foreach (var node in misses)
            {
                missNodes.Add(new CalendarDays()
                {
                    DateStart = node.MissDate,
                    DateEnd= node.MissDate,
                });
            }

            ObservableCollection<CalendarDays> holidaysNodes = new ObservableCollection<CalendarDays>(); 
            foreach (var node in holidays)
            {
                holidaysNodes.Add(new CalendarDays()
                {
                    DateStart = node.StartDate,
                    DateEnd = node.EndDate,
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

    }
}
