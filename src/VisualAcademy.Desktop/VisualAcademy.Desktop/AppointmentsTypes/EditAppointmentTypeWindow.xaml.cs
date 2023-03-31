using System.Windows;

namespace VisualAcademy.Desktop.AppointmentsTypes;

/// <summary>
/// EditAppointmentTypeWindow.xaml에 대한 상호 작용 논리
/// </summary>
public partial class EditAppointmentTypeWindow : Window
{
    // 약속 유형의 이름을 저장하는 속성
    public string AppointmentTypeName { get; private set; }
    // 약속 유형의 활성화 상태를 저장하는 속성
    public new bool IsActive { get; private set; }

    // 생성자를 사용하여 약속 유형 이름 및 활성화 상태를 초기화
    public EditAppointmentTypeWindow(string appointmentTypeName, bool isActive)
    {
        InitializeComponent();
        AppointmentTypeNameTextBox.Text = appointmentTypeName;
        IsActiveCheckBox.IsChecked = isActive;
    }

    // 확인 버튼 클릭 시 처리하는 이벤트 핸들러
    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        // 약속 유형 이름 및 활성화 상태를 저장
        AppointmentTypeName = AppointmentTypeNameTextBox.Text;
        IsActive = (IsActiveCheckBox.IsChecked == true);
        // 대화 상자 결과를 true로 설정하여 사용자가 확인 버튼을 클릭했음을 나타냄
        DialogResult = true;
    }

    // 취소 버튼 클릭 시 처리하는 이벤트 핸들러
    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        // 대화 상자 결과를 false로 설정하여 사용자가 취소 버튼을 클릭했음을 나타냄
        DialogResult = false;
    }
}
