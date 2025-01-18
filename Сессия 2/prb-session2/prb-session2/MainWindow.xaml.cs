using prb_session2.Data;
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
using Newtonsoft.Json;

namespace prb_session2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient _httpClient = new HttpClient();
        private List<Departament> _departaments;
        private List<SubDepartament> _subDepartaments;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task GetData()
        {
            var response = await _httpClient.GetAsync("https://localhost:7208/Departament/GetDepartaments");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _departaments = JsonConvert.DeserializeObject<List<Departament>>(content);
            }
            else {
                throw new Exception("Ошибка при получении данных: " + response.StatusCode);
            }
        }
    }
}
