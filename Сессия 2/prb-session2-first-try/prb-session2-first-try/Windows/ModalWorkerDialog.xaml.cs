using prb_session2_first_try.Models;
using prb_session2_first_try.ClassHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace prb_session2_first_try.Windows
{
    /// <summary>
    /// Логика взаимодействия для ModalWorkerDialog.xaml
    /// </summary>
    public partial class ModalWorkerDialog : Window
    {
        private Worker _worker;
        private HttpClient _httpClient;
        private CalendarNode _calendar;

        public ModalWorkerDialog(Worker worker, HttpClient httpClient)
        {
            InitializeComponent();
            _worker = worker;
            _httpClient = httpClient;
        }

        public async Task GetData()
        {
            string url = $"https://localhost:7208//Workers/GetWorkersCalendar/{_worker.IdWorker}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _calendar = JsonConvert.DeserializeObject<CalendarNode>(content);
            }
            else
            {
                throw new Exception("Ошибка при получении событий данного рабочего.");
            }
        }
    }
}
