using System;
using System.Collections.Generic;
using System.Linq;
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

namespace VisualAcademy.Desktop.AppointmentsTypes {
    /// <summary>
    /// AppointmentsTypesMainForm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AppointmentsTypesMainForm : Window {
        public AppointmentsTypesMainForm() {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            var addWindow = new AddAppointmentTypeWindow();
            if (addWindow.ShowDialog() == true) {
                MessageBox.Show(addWindow.AppointmentTypeName);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            var editWindow = new EditAppointmentTypeWindow();
            editWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Are you sure you want to delete this appointment type?", 
                "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
