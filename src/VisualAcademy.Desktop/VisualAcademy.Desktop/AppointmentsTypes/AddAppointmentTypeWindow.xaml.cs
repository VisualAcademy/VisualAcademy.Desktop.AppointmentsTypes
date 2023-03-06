#nullable disable
using System.Windows;

namespace VisualAcademy.Desktop.AppointmentsTypes;

/// <summary>
/// AddAppointmentTypeWindow.xaml에 대한 상호 작용 논리
/// </summary>
public partial class AddAppointmentTypeWindow : Window {
    public AddAppointmentTypeWindow() => InitializeComponent();

    public string AppointmentTypeName { get; private set; }

    private void OkButton_Click(object sender, RoutedEventArgs e) {
        AppointmentTypeName = AppointmentTypeNameTextBox.Text;
        DialogResult = true;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) => DialogResult = false;
}
