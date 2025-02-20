using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebRoadsApi.Models;

namespace WebRoadsApi.Controllers
{
    public class EventNewsController : Controller
    {
        static List<News> news = new List<News>();

        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };


        public IActionResult Index()
        {
            return View();
        }
        [Route("/api/News/AddNews")]
        [HttpPost]
        public IActionResult PostNews()
        {
            news.Add(new News()
            {
                Id = 1,
                Title = "Водители на трассе М-12 сыграли \"Полёт шмеля\"",
                Body = "Они ехали-ехали и сыграли! Они ехали-ехали и сыграли! Они ехали-\r\nехали и сыграли! Они ехали-ехали и сыграли! Они ехали-ехали и\r\nсыграли!",
                Date = "04.05.2024",
                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBkJigufyq00dk5hZq_acK0ix6Gq5LMj59Kg&s",
                PositiveReactions = 0,
                NegativeReactions = 0
            });
            news.Add(new News()
            {
                Id = 2,
                Title = "Водители на трассе М-12 сыграли \"Полёт шмеля\"",
                Body = "Они ехали-ехали и сыграли! Они ехали-ехали и сыграли! Они ехали-\r\nехали и сыграли! Они ехали-ехали и сыграли! Они ехали-ехали и\r\nсыграли!",
                Date = "04.05.2024",
                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBkJigufyq00dk5hZq_acK0ix6Gq5LMj59Kg&s",
                PositiveReactions = 10,
                NegativeReactions = 10
            });
            news.Add(new News()
            {
                Id = 3,
                Title = "Водители на трассе М-12 сыграли \"Полёт шмеля\"",
                Body = "Они ехали-ехали и сыграли! Они ехали-ехали и сыграли! Они ехали-\r\nехали и сыграли! Они ехали-ехали и сыграли! Они ехали-ехали и\r\nсыграли!",
                Date = "04.05.2024",
                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBkJigufyq00dk5hZq_acK0ix6Gq5LMj59Kg&s",
                PositiveReactions = 8,
                NegativeReactions = 4
            });
            return Ok();
        }


        [Route("/api/News/GetNews")]
        [HttpGet]
        public ActionResult GetNews()
        {

            return Ok(JsonConvert.SerializeObject(news, settings));
        }

        [Route("/api/Events/GetEvents")]
        [HttpGet]
        public ActionResult GetEvents()
        {
            List<TextEvent> events = new List<TextEvent>();
            for (int i = 0; i < 3; i++)
            {
                events.Add(new TextEvent()
                {
                    Title = "Общее совещание в актовом зале",
                    Body = "Все сотрудника отдела “Администраторы”\r\nсобираемся",
                    Date = "04.01.2025",
                    Author = "Петров И. И."
                });
            }

            events.Add(new TextEvent()
            {
                Title = "Общее совещание в актовом зале",
                Body = "Все сотрудника отдела “Администраторы”\r\nсобираемся",
                Date = "13.01.2025",
                Author = "Петров И. И."
            });

            for (int i = 0; i < 5; i++)
            {
                events.Add(new TextEvent()
                {
                    Title = "Общее совещание в актовом зале",
                    Body = "Все сотрудника отдела “Администраторы”\r\nсобираемся",
                    Date = "10.02.2025",
                    Author = "Петров И. И."
                });
            }


            return Ok(JsonConvert.SerializeObject(events, settings));
        }

        [Route("/api/Birthdays/GetBirthdays")]
        [HttpGet]
        public ActionResult GetBirthdays()
        {
            List<Birthday> birthdays = new List<Birthday>();
            birthdays.Add(new Birthday()
            {
                Name = "Satoru",
                BirthDate = new DateTime(2006, 01, 30)
            });

            return Ok(JsonConvert.SerializeObject(birthdays, settings));
        }

        [Route("/api/News/PostPositiveReactions")]
        [HttpPut]
        public ActionResult PostPositiveReactions(int id)
        {
            news[id].PositiveReactions++;
            return Ok(JsonConvert.SerializeObject(news[id], settings));
        }

        [Route("/api/News/PostNegativeReactions")]
        [HttpPut]
        public ActionResult PostNegativeReactions(int id)
        {
            news[id].NegativeReactions++;
            return Ok(JsonConvert.SerializeObject(news[id], settings));
        }
    }
}
