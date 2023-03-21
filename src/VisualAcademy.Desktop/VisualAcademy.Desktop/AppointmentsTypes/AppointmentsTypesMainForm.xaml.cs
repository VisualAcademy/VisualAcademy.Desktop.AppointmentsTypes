using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using VisualAcademy.Desktop.Models;

namespace VisualAcademy.Desktop.AppointmentsTypes; 

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
        LoadDataSp();
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

                    var appointmentType = new AppointmentType {
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
    private void LoadDataSp() {
        _appointmentsTypes.Clear();

        // ADO.NET을 사용하여 데이터 조회
        using (var connection = new SqlConnection(_connectionString)) {
            connection.Open();

            var command = new SqlCommand("AppointmentsTypes_GetAll", connection);

            using (var da = new SqlDataAdapter(command)) {
                var dt = new DataTable();
                da.Fill(dt);
                //AppointmentTypesListView.ItemsSource = dt.DefaultView; 
                //AppointmentTypesListView.ItemsSource = DataTableToList<AppointmentType>(dt); // List<T>
                _appointmentsTypes.AddRange(DataTableToList<AppointmentType>(dt));
            }
        }
        AppointmentTypesListView.Items.Refresh();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e) {
        var addWindow = new AddAppointmentTypeWindow();
        if (addWindow.ShowDialog() == true) {
            using (var con = new SqlConnection(_connectionString)) {
                if (DateTime.Now.Second % 2 == 0) {
                    // 학습 목적으로 ADO.NET 사용 
                    var query = "INSERT INTO AppointmentsTypes (AppointmentTypeName, IsActive, DateCreated) " +
                "VALUES (@AppointmentTypeName, @IsActive, @DateCreated)";
                    var cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@AppointmentTypeName", addWindow.AppointmentTypeName);
                    cmd.Parameters.AddWithValue("@IsActive", true);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now); 
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                else {
                    // 학습 목적으로 저장 프로시저 사용
                    var query = "AppointmentsTypes_Insert";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure; // SP

                    cmd.Parameters.AddWithValue("@AppointmentTypeName", addWindow.AppointmentTypeName);
                    //cmd.Parameters.AddWithValue("@IsActive", true);
                    //cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

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
        if (editWindow.ShowDialog() == true) {
            using (var con = new SqlConnection(_connectionString)) {
                if (DateTime.Now.Second % 2 == 1) {
                    // 인라인 SQL 사용 방식
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
                else {
                    // 저장 프로시저 사용 방식 
                    con.Open();

                    var query = "AppointmentsTypes_Update";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.StoredProcedure; 

                    cmd.Parameters.AddWithValue("@Id", appointmentType.Id);
                    cmd.Parameters.AddWithValue("@AppointmentTypeName", editWindow.AppointmentTypeName);
                    //cmd.Parameters.AddWithValue("@IsActive", editWindow.IsActive);

                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        var appointmentType = (AppointmentType)AppointmentTypesListView.SelectedItem;
        if (appointmentType == null) {
            return;
        }

        if (MessageBox.Show("Are you sure you want to delete this appointment type?",
            "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
            using (var con = new SqlConnection(_connectionString)) {
                if (DateTime.Now.Second == 0) {
                    // 인라인 SQL: Ad Hoc 쿼리 
                    con.Open();

                    var query = "DELETE FROM AppointmentsTypes WHERE Id=@Id";
                    var cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Id", appointmentType.Id);

                    cmd.ExecuteNonQuery(); 
                }
                else {
                    // 저장 프로시저
                    con.Open();

                    var query = "AppointmentsTypes_Delete";
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandType= CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", appointmentType.Id);

                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }
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

    /// <summary>
    /// DataTable 형식을 List<typeparamref name="T"/> 형식으로 변경시켜주는 헬퍼 펑션 
    /// https://www.memoengine.com/blog/c%EC%97%90%EC%84%9C-datatable%EC%9D%84-listt-%ED%98%95%ED%83%9C%EB%A1%9C-%EB%B3%80%ED%99%98%ED%95%98%EB%8A%94-%EB%B0%A9%EB%B2%95/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static List<T> DataTableToList<T>(DataTable table) where T : new() {
        List<T> list = new List<T>();

        foreach (DataRow row in table.Rows) {
            T obj = new T();

            foreach (DataColumn column in table.Columns) {
                PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);

                if (prop != null && row[column] != DBNull.Value) {
                    prop.SetValue(obj, row[column], null);
                }
            }

            list.Add(obj);
        }

        return list;
    }
}
