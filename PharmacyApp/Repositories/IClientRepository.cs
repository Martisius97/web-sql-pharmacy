using PharmacyApp.Models;

namespace PharmacyApp.Repositories;

public interface IClientRepository
{
    public Task<List<Client>> GetClientsAsync();
    public Task InsertClientAsync(Client client);
    public Task InsertClientsAsync(IEnumerable<Client> clients);
    public Task UpdateClientAsync(Client client);
}