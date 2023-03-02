using Microsoft.Data.SqlClient;
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
using VisualAcademy.Desktop.Models;

namespace VisualAcademy.Desktop.AppointmentsTypes {
    /// <summary>
    /// AppointmentsTypesMainForm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AppointmentsTypesMainForm : Window {

        private readonly List<AppointmentType> _appointmentsTypes;

        private readonly string _connectionString = 
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppointmentDatabase;Integrated Security=True";

        public AppointmentsTypesMainForm() {
            InitializeComponent();

            _appointmentsTypes = new List<AppointmentType>();
            AppointmentTypesListView.ItemsSource = _appointmentsTypes;
            LoadData();
        }

        private void LoadData() {
            _appointmentsTypes.Clear();

            // ADO.NET을 사용하여 데이터 조회
            using (var connection = new SqlConnection(_connectionString)) {
                connection.Open();

                var command = new SqlCommand("SELECT Id, AppointmentTypeName, IsActive FROM AppointmentsTypes", connection);

                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var id = (int)reader["Id"];
                        var name = (string)reader["AppointmentTypeName"];
                        var isActive = (bool)reader["IsActive"];

                        var appointmentType = new AppointmentType 
                        { 
                            Id = id,
                            AppointmentTypeName = name,
                            IsActive = isActive
                        };
                        _appointmentsTypes.Add(appointmentType);
                    }
                }
            }
            AppointmentTypesListView.Items.Refresh(); 
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            var addWindow = new AddAppointmentTypeWindow();
            if (addWindow.ShowDialog() == true) {
                using (var con = new SqlConnection(_connectionString)) {
                    con.Open();

                    var query = "INSERT INTO AppointmentsTypes (AppointmentTypeName, IsActive, DateCreated) " +
                        "VALUES (@AppointmentTypeName, @IsActive, @DateCreated)";
                    var cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@AppointmentTypeName", addWindow.AppointmentTypeName);
                    cmd.Parameters.AddWithValue("@IsActive", true);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            var appointmentType = (AppointmentType)AppointmentTypesListView.SelectedItem;
            if (appointmentType == null) {
                return;
            }

            var editWindow = new EditAppointmentTypeWindow(
                appointmentType.AppointmentTypeName, appointmentType.IsActive);
            if (editWindow.ShowDialog() == true) 
            {
                using (var con = new SqlConnection(_connectionString)) {
                    con.Open();

                    var query = "UPDATE AppointmentsTypes " +
                        "SET AppointmentTypeName=@AppointmentTypeName, IsActive=@IsActive " +
                        "WHERE Id=@Id";
                    var cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Id", appointmentType.Id);
                    cmd.Parameters.AddWithValue("@AppointmentTypeName", editWindow.AppointmentTypeName);
                    cmd.Parameters.AddWithValue("@IsActive", editWindow.IsActive);

                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Are you sure you want to delete this appointment type?", 
                "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        private void AppointmentTypesListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var appointmentType = (AppointmentType)AppointmentTypesListView.SelectedItem;
            if (appointmentType == null) {
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
            else {
                EditButton.IsEnabled = true; 
                DeleteButton.IsEnabled = true;
            }
        }
    }
}
