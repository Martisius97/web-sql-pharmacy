using PharmacyApp.Models;

namespace PharmacyApp.Repositories;

public interface ILogsRepository
{
    public Task InsertLogEntryAsync(LogEntry log);
}