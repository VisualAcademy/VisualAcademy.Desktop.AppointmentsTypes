#nullable disable

namespace VisualAcademy.Desktop.Models; 
public class AppointmentType {
    public int Id { get; set; }
    public string AppointmentTypeName { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime DateCreated { get; set; } = DateTime.Now;
}
