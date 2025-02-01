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
            txbFullName.Text = _worker.FullName;
            txbEmail.Text = _worker.Email;
            txbWorkPhoneNumber.Text = _worker.WorkPhoneNumber;
            txbPrivatePhoneNumber.Text = _workerPrivateInfo.PrivatePhoneNumber;
            dpBirthday.Text = _workerPrivateInfo.Birthday.ToString();

            var positions = await ApiHelper.GetPositions();
            cmbPosition.ItemsSource = positions;
            cmbPosition.SelectedValue = positions.FirstOrDefault(x => x.IdPosition == _worker.IdPosition);

            var cabinets = await ApiHelper.GetCabinets();
            cmbCabinet.ItemsSource = cabinets;
            cmbCabinet.SelectedValue = cabinets.FirstOrDefault(x => x.IdCabinet == _worker.IdCabinet);

            var departaments = await ApiHelper.GetDepartaments();
            cmbDepartament.ItemsSource = departaments;
            cmbDepartament.SelectedValue = departaments.FirstOrDefault(x => x.IdDepartament == _worker.IdDepartament);

            var workers = await ApiHelper.GetWorkers(1);
            cmbLeader.ItemsSource = workers;
            cmbLeader.SelectedValue = workers.FirstOrDefault(x => x.IdWorker == _worker.IdLead);

            cmbHelper.ItemsSource = workers;
            cmbHelper.SelectedValue = workers.FirstOrDefault(x => x.IdWorker == _worker.IdHelper);

            treeCalendar.ItemsSource = _calendar;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _worker = await ApiHelper.GetWorker(_id);
            _workerPrivateInfo = await ApiHelper.GetWorkerPrivateInfo(_id);
            _calendar = await ApiHelper.GetWorkerCalendar(_id, "present");

            LoadData();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ButtonPresentCalendar_Click(object sender, RoutedEventArgs e)
        {
            _calendar = await ApiHelper.GetWorkerCalendar(_id, "present");

            treeCalendar.ItemsSource = null;
            treeCalendar.ItemsSource = _calendar;
        }

        private async void ButtonPastCalendar_Click(object sender, RoutedEventArgs e)
        {
            _calendar = await ApiHelper.GetWorkerCalendar(_id, "past");

            treeCalendar.ItemsSource = null;
            treeCalendar.ItemsSource = _calendar;
        }

        private async void ButtonFutureClick_Click(object sender, RoutedEventArgs e)
        {
            _calendar = await ApiHelper.GetWorkerCalendar(_id, "future");

            treeCalendar.ItemsSource = null;
            treeCalendar.ItemsSource = _calendar;
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
    }
}
