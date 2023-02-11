using DbUp;
using Microsoft.Extensions.Options;
using PharmacyApp.Migration.Scripts;
using PharmacyApp.Options;

namespace PharmacyApp.Migration;

public class DbMigrationClient : IDbMigrationClient
{
    private readonly string? _sqlConnectionString;

    public DbMigrationClient(IOptions<SqlConnectionOptions> options)
    {
        _sqlConnectionString = options.Value.SqlConnectionString;
    }

    public void PerformMigrations()
    {
        ApplyMigrations();
    }
    
    private void ApplyMigrations()
    {
        var upgrader =
            DeployChanges.To
                .SqlDatabase(_sqlConnectionString)
                .WithScript("schema script", new Script1_Schema())
                .WithTransaction()
                .Build();
        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            throw result.Error;
        }
    }
}