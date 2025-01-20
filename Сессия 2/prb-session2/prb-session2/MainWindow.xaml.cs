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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetData();
            //GraphDrawing();
            GraphDrawingI();
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

        private void GraphDrawing()
        {
            Dictionary<int, List<Departament>> departamnetLevels = new Dictionary<int, List<Departament>>();

            foreach(var departament in _departaments)
            {
                if (!departamnetLevels.ContainsKey(departament.LevelDepartament)) departamnetLevels[departament.LevelDepartament] = new List<Departament>() { departament };
                else {
                    departamnetLevels[departament.LevelDepartament].Add(departament);
                }
            }

            var rectangleHeight = 40;
            var rectangleWidth = 200;

            var levelsCount = departamnetLevels.Count;
            var height = levelsCount * rectangleHeight + ((levelsCount + (levelsCount - 1)) * 10);

            var maxLevelSize = departamnetLevels.Values.Max(x => x.Count);
            var width = maxLevelSize * rectangleWidth + ((maxLevelSize + (maxLevelSize - 1)) * 10);

            graphCanvas.Height = height;
            graphCanvas.Width = width;

            foreach(var levels in departamnetLevels)
            {
                var departaments = levels.Value;
                var center = width / 2;
                var rectanglesCount = departaments.Count;
                var rectangles = center - (rectanglesCount * (rectangleWidth / 2));
                var startPosition = rectanglesCount == 1 ? rectangles : rectangles - (rectanglesCount * 10);
                var topPosition = (levels.Key) * rectangleHeight + ((levels.Key + 1) * 10);
                 
                for(int i = 0; i < departaments.Count; i++)
                {
                    Button btnDepartament = new Button();
                    btnDepartament.Height = rectangleHeight;
                    btnDepartament.Width = rectangleWidth;
                    btnDepartament.Style = (Style)Application.Current.FindResource("btnStyle");
                    btnDepartament.Content = departaments[i].NameDepartament;

                    var leftPosition = rectangles + (rectangleWidth * i + 10 * i);

                    Canvas.SetLeft(btnDepartament, leftPosition);
                    Canvas.SetTop(btnDepartament, topPosition);

                    graphCanvas.Children.Add(btnDepartament);
                }
            }
        }

        private void GraphDrawingI()
        {
            Dictionary<int, List<Departament>> departamnetLevels = new Dictionary<int, List<Departament>>();
            Dictionary<int, List<int>> subDepartaments = new Dictionary<int, List<int>>();

            foreach (var departament in _departaments)
            {
                if (!departamnetLevels.ContainsKey(departament.LevelDepartament)) departamnetLevels[departament.LevelDepartament] = new List<Departament>() { departament };
                else
                {
                    departamnetLevels[departament.LevelDepartament].Add(departament);
                }
            }

            foreach(var d in _subDepartaments)
            {
                if (!subDepartaments.ContainsKey(d.IdMainDepartament)) subDepartaments[d.IdMainDepartament] = new List<int>() { d.IdDaughterDepartament };
                else
                {
                    subDepartaments[d.IdMainDepartament].Add(d.IdDaughterDepartament);
                }
            }

            var rectangleHeight = 40;
            var rectangleWidth = 200;

            var levelsCount = departamnetLevels.Count;
            var height = levelsCount * rectangleHeight + ((levelsCount + (levelsCount - 1)) * 10);

            var maxLevelSize = departamnetLevels.Values.Max(x => x.Count);
            var width = maxLevelSize * rectangleWidth + ((maxLevelSize + (maxLevelSize - 1)) * 10);

            graphCanvas.Height = height;
            graphCanvas.Width = width;

            Deec(subDepartaments,departamnetLevels,width/2);




            //foreach (var levels in departamnetLevels)
            //{
            //    var departaments = levels.Value;
            //    var center = width / 2;
            //    var rectanglesCount = departaments.Count;
            //    var rectangles = center - (rectanglesCount * (rectangleWidth / 2));
            //    var startPosition = rectanglesCount == 1 ? rectangles : rectangles - (rectanglesCount * 10);
            //    var topPosition = (levels.Key) * rectangleHeight + ((levels.Key + 1) * 10);

            //    for (int i = 0; i < departaments.Count; i++)
            //    {
            //        Button btnDepartament = new Button();
            //        btnDepartament.Height = rectangleHeight;
            //        btnDepartament.Width = rectangleWidth;
            //        btnDepartament.Style = (Style)Application.Current.FindResource("btnStyle");
            //        btnDepartament.Content = departaments[i].NameDepartament;

            //        var leftPosition = rectangles + (rectangleWidth * i + 10 * i);

            //        Canvas.SetLeft(btnDepartament, leftPosition);
            //        Canvas.SetTop(btnDepartament, topPosition);

            //        graphCanvas.Children.Add(btnDepartament);
            //    }
            //}
        }

        private void Deec(Dictionary<int, List<int>> listD, Dictionary<int,List<Departament>> listDLevel, int center)
        {

            var rectangleHeight = 40;
            var rectangleWidth = 200;


            var levelsCount = listDLevel.Count;
            var height = levelsCount * rectangleHeight + ((levelsCount + (levelsCount - 1)) * 10);


            var maxLevelSize = listDLevel.Values.Max(x => x.Count);
            var width = maxLevelSize * rectangleWidth + ((maxLevelSize + (maxLevelSize - 1)) * 10);


            graphCanvas.Height = height;
            graphCanvas.Width = width;



            var topPosition = (levels.Key) * rectangleHeight + ((levels.Key + 1) * 10);

            List<int> keys = listDLevel.Keys.ToList();

            foreach(var key in keys)
            {
                foreach(var values in listDLevel[key])
                {
                    foreach()
                    {

                    }

                    Button btnDepartament = new Button();

                    // Размеры кнопки
                    btnDepartament.Height = rectangleHeight;
                    btnDepartament.Width = rectangleWidth;

                    btnDepartament.Style = (Style)Application.Current.FindResource("btnStyle");

                    //Данные в кнопке
                    btnDepartament.Content = listDLevel.First(x => x.Value == key).NameDepartament;

                    //Позиция кнопки
                    var rectangles = center - (rectanglesCount * (rectangleWidth / 2));
                    var leftPosition = rectangles + (rectangleWidth * i + 10 * i);



                    Canvas.SetLeft(btnDepartament, leftPosition);
                    Canvas.SetTop(btnDepartament, topPosition);

                    graphCanvas.Children.Add(btnDepartament);



                }

            }

          


            }

        }

    }
}
