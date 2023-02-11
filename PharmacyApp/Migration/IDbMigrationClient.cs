namespace PharmacyApp.Migration;

public interface IDbMigrationClient
{
    void PerformMigrations();
}