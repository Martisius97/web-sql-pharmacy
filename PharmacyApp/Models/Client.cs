namespace PharmacyApp.Models;

public record Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string? PostCode { get; set; }
}