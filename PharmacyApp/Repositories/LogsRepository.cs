using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PharmacyApp.Options;
using PharmacyApp.Models;

namespace PharmacyApp.Repositories;

public class LogsRepository : ILogsRepository
{
    private readonly string? _connectionString;

    public LogsRepository(IOptions<SqlConnectionOptions> options)
    {
        _connectionString = options.Value.SqlConnectionString;
    }
    
    public async Task InsertLogEntryAsync(LogEntry log)
    {
        using var sqlConnection = new SqlConnection(_connectionString);
        await sqlConnection.OpenAsync();
        var command = sqlConnection.CreateCommand();
        var date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
        command.CommandText = $"INSERT INTO [dbo].[Logs] ([Id], [Action], [Date]) VALUES (N'{Guid.NewGuid()}',N'{log.Action}',N'{date}')";
        await command.ExecuteNonQueryAsync();
        await sqlConnection.CloseAsync();
    }
    
    public async Task InsertLogEntries(List<LogEntry> logs)
    {
        using var sqlConnection = new SqlConnection(_connectionString);
        await sqlConnection.OpenAsync();
        var command = sqlConnection.CreateCommand();
        command.CommandText = $"INSERT INTO [dbo].[Logs] ([Id], [Action], [Date]) VALUES ";
        await command.ExecuteNonQueryAsync();
        await sqlConnection.CloseAsync();
    }
}