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
using prb_session2.ModelHelpers;
using System.Reflection.Emit;

namespace prb_session2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient _httpClient = new HttpClient();
        private List<Departament> _departaments;
        private List<GraphElement> _graphElements;

        public MainWindow()
        {
            InitializeComponent();
            _graphElements = new List<GraphElement>();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetData();
            //GraphDrawing();
            //GraphDrawingI();
            ParseDataToElements();
            ElementDrawing();
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

        private void ParseDataToElements()
        {
            GraphElement.LevelsCount = 0;
            var elementDict = new Dictionary<int, GraphElement>();

            // Создаем элементы графа и добавляем их в словарь
            foreach (var departament in _departaments)
            {
                var element = new GraphElement()
                {
                    Parent = departament,
                    Children = new List<GraphElement>()
                };
                _graphElements.Add(element);
                elementDict[departament.IdDepartament] = element; // Сохраняем элемент по IdDepartament
            }

            foreach (var departament in _departaments)
            {
                if (departament.IdParentDepartament != null)
                {
                    if (elementDict.TryGetValue(departament.IdParentDepartament.Value, out var parentElement))
                    {
                        if(parentElement.Children.Count == 0)
                        {
                            GraphElement.LevelsCount++;
                        }
                        parentElement.Children.Add(elementDict[departament.IdDepartament]); 
                    }
                }
                else
                {
                    GraphElement.LevelsCount++; 
                }
            }

            foreach (var element in _graphElements.Where(e => e.Parent.IdParentDepartament == null))
            {
                SetLevel(element, 1);
            }
        }

        private void SetLevel(GraphElement element, int level)
        {
            element.Level = level;

            foreach (var child in element.Children)
            {
                SetLevel(child, level + 1);
            }
        }


        private void ElementDrawing()
        {
            GraphElement.HeightElement = 40;
            GraphElement.WidthElement = 200;
            GraphElement.ElementSpace = 20;

            var levelWidths = new Dictionary<int, int>();

            foreach (var element in _graphElements)
            {
                if (!levelWidths.ContainsKey(element.Level))
                {
                    levelWidths[element.Level] = 0;
                }
                int elementWidth = element.Width;

                if (elementWidth > levelWidths[element.Level])
                {
                    levelWidths[element.Level] = elementWidth;
                } else
                {
                    levelWidths[element.Level] += 20 + elementWidth;
                }
            }

            int totalWidth = levelWidths.Values.Sum() + (levelWidths.Count - 1) * GraphElement.ElementSpace;

            graphCanvas.Height = GraphElement.Height;
            graphCanvas.Width = totalWidth + GraphElement.ElementSpace * 2;

            graphCanvas.Children.Clear();

            foreach (var element in _graphElements)
            {
                Button btnDepartament = new Button();
                btnDepartament.Height = GraphElement.HeightElement;
                btnDepartament.Width = GraphElement.WidthElement;
                btnDepartament.Style = (Style)Application.Current.FindResource("btnStyle");
                btnDepartament.Content = element.Parent.NameDepartament;

                Canvas.SetLeft(btnDepartament, element.GetXPosition(_graphElements, levelWidths, (int)graphCanvas.Width));
                Canvas.SetTop(btnDepartament, element.GetYPosition());

                graphCanvas.Children.Add(btnDepartament);
            }
        }


    }
}
