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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace prb_session2_first_try.Windows
{
    /// <summary>
    /// Логика взаимодействия для ModalWorkerDialog.xaml
    /// </summary>
    public partial class ModalWorkerDialog : Window
    {
        private Worker _worker;
        private WorkerPrivateInfo _workerPrivateInfo;
        private ObservableCollection<CalendarNode> _calendar;
        private int _id;

        public ModalWorkerDialog(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private async void LoadData()
        {
            _worker = await ApiHelper.GetWorker(_id);
            _workerPrivateInfo = await ApiHelper.GetWorkerPrivateInfo(_id);
            _calendar = await ApiHelper.GetWorkerCalendar(_id);
            treeCalendar.ItemsSource = _calendar;

            txbFullName.DataContext = _worker;
            txbEmail.DataContext = _worker;
            txbPrivatePhoneNumber.DataContext = _workerPrivateInfo;
            txbBirthday.DataContext = _workerPrivateInfo;
            LoadApiData();
        }

        private async void LoadApiData()
        {
            var positions = await ApiHelper.GetPositions();
            cmbPosition.ItemsSource = positions;
            cmbPosition.SelectedValue = positions.FirstOrDefault(x => x.IdPosition == _worker.IdPosition);

            var cabinets = await ApiHelper.GetCabinets();
            cmbCabinet.ItemsSource = cabinets;
            cmbCabinet.SelectedValue = cabinets.FirstOrDefault(x => x.IdCabinet == _worker.IdCabinet);

            var departaments = await ApiHelper.GetDepartaments();
            cmbDepartament.ItemsSource = departaments;
            cmbDepartament.SelectedValue = departaments.FirstOrDefault(x => x.IdDepartament == _worker.IdDepartament);

            var workers = await ApiHelper.GetWorkersFromDepartament(_worker.IdDepartament);
            workers.Insert(0, null);
            cmbLeader.ItemsSource = workers;
            cmbLeader.SelectedValue = workers.Where(x => x != null).FirstOrDefault(x => x.IdWorker == _worker.IdLead);

            cmbHelper.ItemsSource = workers;
            cmbHelper.SelectedValue = workers.Where(x => x != null).FirstOrDefault(x => x.IdWorker == _worker.IdHelper);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetElementsChangable(DependencyObject parent)
        {
            if (parent == null) return;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is UIElement uiElement && !(child is Button))
                {
                    uiElement.IsHitTestVisible = true;
                }
                SetElementsChangable(child);
            }
        }

        private void ButtonEditMode_Click(object sender, RoutedEventArgs e)
        {
            SetElementsChangable(gridInfo);

            btnEdit.Visibility = Visibility.Collapsed;

            btnEdit.Visibility = Visibility.Visible;
            btnRetire.Visibility = Visibility.Visible;
        }

        private void gridInfo_Error(object sender, ValidationErrorEventArgs e)
        {
            Grid grid = sender as Grid;
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = sender as ComboBox;
            var tag = cmb.Tag as string;         
            switch(tag)
            {
                case "Departament":
                    _worker.IdDepartament = (cmb.SelectedValue as Departament).IdDepartament;
                    break;
                case "Position":
                    _worker.IdPosition = (cmb.SelectedValue as Position).IdPosition;
                    break;
                case "Cabinet":
                    _worker.IdCabinet = (cmb.SelectedValue as Cabinet).IdCabinet;
                    break;
                case "Leader":
                case "Helper":
                    if(cmb.SelectedValue == null)
                    {
                        break;
                    }
                    _worker.IdHelper = (cmb.SelectedValue as Worker).IdWorker;
                    break;
            }
        }

        private async void ButtonDeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(await ApiHelper.DeleteWorker(_id));
                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("При увольнении произошла ошибка: " + ex.Message);
            }
        }

        private bool GetErrors(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                UIElement element = child as UIElement;
                if (element == null) continue;

                if (Validation.GetHasError(element))
                {
                    return true;
                }

                GetErrors(element);
            }
            return false;
        }

        private async void ButtonEditWorker_Click(object sender, RoutedEventArgs e)
        {
            if(GetErrors(gridInfo))
            {
                MessageBox.Show("Не все данные введены корректно.");
                return;
            }
            try
            {
                MessageBox.Show(await ApiHelper.PutWorker(_id, _worker));
                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("При изменении произошла ошибка: " + ex.Message);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CalendarGenerate(panelCheckBoxes);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CalendarGenerate(panelCheckBoxes);
        }

        private void CalendarGenerate(DependencyObject parent)
        {
            var sortedCalendar = new ObservableCollection<CalendarNode>();

            foreach (object child in LogicalTreeHelper.GetChildren(parent))
            {
                if (!(child is CheckBox checkBox) || (bool)(!checkBox.IsChecked)) continue;

                switch (checkBox.Tag?.ToString())
                {
                    case "Past":
                        AddFilteredNodes(sortedCalendar, date => date.DateEnd < DateTime.Today);
                        break;
                    case "Present":
                        AddFilteredNodes(sortedCalendar, date => date.DateStart <= DateTime.Today && date.DateEnd >= DateTime.Today);
                        break;
                    case "Future":
                        AddFilteredNodes(sortedCalendar, date => date.DateStart > DateTime.Today);
                        break;
                }
            }

            foreach(var node in sortedCalendar)
            {
                node.Days.OrderBy(x => x.DateStart);
            }

            treeCalendar.ItemsSource = null;
            treeCalendar.ItemsSource = sortedCalendar;
            
        }

        private void AddFilteredNodes(ObservableCollection<CalendarNode> sortedCalendar, Func<CalendarDays, bool> filter)
        {
            foreach (var calendarNode in _calendar)
            {
                var filteredDays = calendarNode.Days.Where(filter).ToList();

                if (filteredDays.Count > 0)
                { 
                    var existingNode = sortedCalendar.FirstOrDefault(node => node.Name == calendarNode.Name);

                    if (existingNode != null)
                    {
                        foreach (var day in filteredDays)
                        {
                            if (!existingNode.Days.Contains(day))
                            {
                                existingNode.Days.Add(day);
                            }
                        }
                    }
                    else
                    {
                        var newNode = new CalendarNode
                        {
                            Name = calendarNode.Name,
                            Days = new ObservableCollection<CalendarDays>(filteredDays)
                        };
                        sortedCalendar.Add(newNode);
                    }
                }
            }
        }
    }
}