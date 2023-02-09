namespace PharmacyApp.Models;

public class LogEntry
{
    public Guid Id { get; set; }
    public string Action { get; set; }
    public DateTime Date { get; set; }
}