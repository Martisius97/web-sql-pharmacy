namespace PharmacyApp.Migration;

public interface IDbMigrationClient
{
    void PerformMigrationsAsync();
}