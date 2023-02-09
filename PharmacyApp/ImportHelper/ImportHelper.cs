using PharmacyApp.Repositories;

namespace PharmacyApp.Models;

public class ImportHelper : IImportHelper
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogsRepository _logsRepository;
    
    private const int BatchSize = 100;

    public ImportHelper(IClientRepository clientRepository, ILogsRepository logsRepository)
    {
        _clientRepository = clientRepository;
        _logsRepository = logsRepository;
    }

    public async Task ImportClientsAsync(List<Client> clients)
    {
        int currentBatch = 0;
        do
        {
            var clientBatch = clients.Skip(BatchSize * currentBatch).Take(BatchSize);
            await _clientRepository.InsertClientsAsync(clientBatch);
            await LogAboutInsert(clients);
            currentBatch++;
        } while (clients.Count > currentBatch * BatchSize);

        // await _clientRepository.InsertClientsAsync(clients.Skip(BatchSize * currentBatch).Take(BatchSize));
    }

    private async Task LogAboutInsert(IEnumerable<Client> clients)
    {
        foreach (var client in clients)
        {
            await _logsRepository.InsertLogEntryAsync(new LogEntry() {Action = $"Client was inserted  {client.Name}"});
        }
    }
}