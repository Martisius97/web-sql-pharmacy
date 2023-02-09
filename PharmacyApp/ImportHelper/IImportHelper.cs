namespace PharmacyApp.Models;

public interface IImportHelper
{
    Task ImportClientsAsync(List<Client> clients);
}