using Newtonsoft.Json;
using prb_session2_first_try.ClassHelpers;
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
        private List<Departament> _departaments;
        private List<GraphElement> _graphElements;
        private List<Button> _canvasButtons;

        public MainWindow()
        {
            InitializeComponent();
            _graphElements = new List<GraphElement>();
            _canvasButtons = new List<Button>();
            ApiHelper.SetHttpClient(new HttpClient());
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _departaments = await ApiHelper.GetDepartaments();
            ParseDataToElements();
            ElementDrawing();
            LineDrawing(0);
        }


        private void ParseDataToElements()
        {
            GraphElement.LevelsCount = 0;
            var elementDict = new Dictionary<int, GraphElement>();

            foreach (var departament in _departaments)
            {
                var element = new GraphElement()
                {
                    Parent = departament,
                    Children = new List<GraphElement>()
                };
                _graphElements.Add(element);
                elementDict[departament.IdDepartament] = element;
            }

            foreach (var departament in _departaments)
            {
                if (departament.IdParentDepartament != null)
                {
                    if (elementDict.TryGetValue(departament.IdParentDepartament.Value, out var parentElement))
                    {
                        if (parentElement.Children.Count == 0)
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
            GraphElement.ElementSpace = 40;

            var levelWidths = new Dictionary<int, int>();

            foreach (var element in _graphElements)
            {
                if (!levelWidths.ContainsKey(element.Level))
                {
                    levelWidths[element.Level] = 0;
                }
                int elementWidth = element.Width;

                if (levelWidths[element.Level] == 0)
                {
                    levelWidths[element.Level] = elementWidth;
                }
                else
                {
                    levelWidths[element.Level] += 20 + elementWidth;
                }
            }

            int totalWidth = levelWidths.Values.Max();

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
                btnDepartament.Tag = element.Parent.IdDepartament;
                btnDepartament.Click += GraphElement_Click;

                Canvas.SetLeft(btnDepartament, element.GetXPosition(_graphElements, levelWidths, (int)graphCanvas.Width));
                Canvas.SetTop(btnDepartament, element.GetYPosition());

                _canvasButtons.Add(btnDepartament);

                graphCanvas.Children.Add(btnDepartament);
            }
        }
        private async void GraphElement_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            listWorkers.ItemsSource = null;
            listWorkers.ItemsSource = await LoadWorkerInfo((int)button.Tag);
        }
        private void LineDrawing(int index)
        {
            if (index == _graphElements.Count)
            {
                return;
            }
            var element = _graphElements[index];
            foreach (var children in _graphElements[index].Children)
            {
                var parent = _canvasButtons.Where(x => x.Content == element.Parent.NameDepartament).FirstOrDefault();
                if (parent == default)
                {
                    return;
                }
                var XParent = Canvas.GetLeft(parent) + GraphElement.WidthElement / 2;
                var YParent = Canvas.GetTop(parent) + GraphElement.HeightElement;

                var child = _canvasButtons.Where(x => x.Content == children.Parent.NameDepartament).FirstOrDefault();
                if (child == default)
                {
                    return;
                }
                var XChild = Canvas.GetLeft(child) + GraphElement.WidthElement / 2;
                var YChild = Canvas.GetTop(child);

                Line mainLine = new Line()
                {
                    X1 = XParent,
                    Y1 = YParent,
                    X2 = XChild,
                    Y2 = YChild - 5,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                };

                PointCollection points = new PointCollection();
                points.Add(new Point()
                {
                    X = XChild,
                    Y = YChild,
                });
                points.Add(new Point()
                {
                    X = XChild - 5,
                    Y = YChild - 5,
                });
                points.Add(new Point()
                {
                    X = XChild + 5,
                    Y = YChild - 5,
                });
                points.Add(new Point()
                {
                    X = XChild,
                    Y = YChild,
                });

                Polygon triangle = new Polygon()
                {
                    Points = points,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Fill = Brushes.Black,
                };

                graphCanvas.Children.Add(mainLine);
                graphCanvas.Children.Add(triangle);
            }
            LineDrawing(index + 1);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;
            ModalWorkerDialog dialog = new ModalWorkerDialog((int)grid.Tag);
            dialog.ShowDialog();
        }

        private async Task<List<WorkerInfo>> LoadWorkerInfo(int id)
        {
            var workers = await ApiHelper.GetWorkersFromDepartamentAndChilds(id);
            var cabinets = await ApiHelper.GetCabinets();
            var positions = await ApiHelper.GetPositions();

            List<WorkerInfo> info = new List<WorkerInfo>();

            foreach(var w in workers)
            {
                info.Add(new WorkerInfo()
                {
                    Worker = w,
                    Departament = _departaments.FirstOrDefault(x => x.IdDepartament == w.IdDepartament),
                    Position = positions.FirstOrDefault(x => x.IdPosition == w.IdPosition),
                    Cabinet = cabinets.FirstOrDefault(x => x.IdCabinet == w.IdCabinet)
                });
            }

            return info;
        }
    }

    public class WorkerInfo
    {
        public Worker Worker { get; set; }
        public Departament Departament { get; set; }
        public Position Position { get; set; }
        public Cabinet Cabinet { get; set; }
    }
}
