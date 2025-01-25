using Newtonsoft.Json;
using prb_session2_first_try.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prb_session2_first_try.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient _httpClient;
        private List<Worker> _workers = new List<Worker>();

        public MainWindow()
        {
            _httpClient = new HttpClient();
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetData();

            listWorkers.ItemsSource = _workers;
        }

        private async Task GetData()
        {
            string url = "https://localhost:7208/Workers/GetWorkers";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content  = await response.Content.ReadAsStringAsync();
                _workers = JsonConvert.DeserializeObject<List<Worker>>(content);
            }
            else
            {
                throw new Exception($"Ошибка при получении данных. Код ошибки: {response.StatusCode}. Текст ошибки: {response.Content}.");
            }
        }

        private void listWorkers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
