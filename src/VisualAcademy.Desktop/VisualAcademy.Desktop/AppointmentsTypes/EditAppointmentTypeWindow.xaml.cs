using System.Windows;

namespace VisualAcademy.Desktop.AppointmentsTypes;

/// <summary>
/// EditAppointmentTypeWindow.xaml에 대한 상호 작용 논리
/// </summary>
public partial class EditAppointmentTypeWindow : Window {

    public string AppointmentTypeName { get; private set; }
    public new bool IsActive { get; private set; }

    public EditAppointmentTypeWindow(string appointmentTypeName, bool isActive) {
        InitializeComponent();
        AppointmentTypeNameTextBox.Text = appointmentTypeName;
        IsActiveCheckBox.IsChecked = isActive;
    }

    private void OkButton_Click(object sender, RoutedEventArgs e) {
        AppointmentTypeName = AppointmentTypeNameTextBox.Text;
        IsActive = (IsActiveCheckBox.IsChecked == true);
        DialogResult = true;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;
}
