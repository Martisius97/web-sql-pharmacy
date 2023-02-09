using Microsoft.Extensions.Options;
using PharmacyApp.Migration;
using PharmacyApp.Options;
using PharmacyApp.Providers;

namespace PharmacyApp.Registrations;

public static class StorageRegistration
{
    public static OptionsBuilder<SqlConnectionOptions> RegisterStorage(this IServiceCollection services)
    {
        return services
            .AddSingleton<ISqlProvider, SqlProvider>()
            .AddSingleton<IValidateOptions<SqlConnectionOptions>, SqlConnectionOptionsValidator>()
            .AddSingleton<IDbMigrationClient, DbMigrationClient>()
            .AddOptions<SqlConnectionOptions>();
    }
}