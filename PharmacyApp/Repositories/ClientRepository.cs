using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PharmacyApp.Models;
using PharmacyApp.Options;

namespace PharmacyApp.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ILogsRepository _logsRepository;
    private readonly string? _connectionString;

    public ClientRepository(IOptions<SqlConnectionOptions> options, ILogsRepository logsRepository)
    {
        _logsRepository = logsRepository;
        _connectionString = options.Value.SqlConnectionString;
    }
    
    public async Task<List<Client>> GetClientsAsync()
    {
        await using var sqlConnection = new SqlConnection(_connectionString);
        await sqlConnection.OpenAsync();
        var command = sqlConnection.CreateCommand();
        command.CommandText = "SELECT * FROM [dbo].[Client]";
        await using var reader = await command.ExecuteReaderAsync();
        IEnumerable<Client>? clients = reader.Parse<Client>();
        return clients.ToList();
    }

    public async Task InsertClientsAsync(IEnumerable<Client> clients)
    {
        using var sqlConnection = new SqlConnection(_connectionString);
        await sqlConnection.OpenAsync();
        var command = sqlConnection.CreateCommand();
        command.CommandText = $"INSERT INTO [dbo].[Client] ([Id], [Name], [Address], [PostCode]) VALUES {GenerateClientsCommand(clients)}";
        await command.ExecuteNonQueryAsync();
        await sqlConnection.CloseAsync();
    }

    public async Task UpdateClientAsync(Client client)
    {
        using var sqlConnection = new SqlConnection(_connectionString);
        await sqlConnection.OpenAsync();
        var command = sqlConnection.CreateCommand();
        command.CommandText = $"UPDATE [dbo].[Client] SET PostCode = '{client.PostCode}' WHERE ID = '{client.Id}'";
        await command.ExecuteNonQueryAsync();
        await sqlConnection.CloseAsync();
        await _logsRepository.InsertLogEntryAsync(new LogEntry() { Action = $"Updated Post Code for {client.Name}"});
    }
    
    private string GenerateClientsCommand(IEnumerable<Client> clients)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var client in clients)
        {
            sb.Append($"(N'{Guid.NewGuid()}',N'{client.Name}',N'{client.Address}',N'{client.PostCode}'),");
        }

        if (sb.Length > 0)
        {
            sb.Length--;
        }
        
        return sb.ToString();
    }
}