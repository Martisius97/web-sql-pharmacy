using Microsoft.Extensions.Options;
using PharmacyApp.Options;
using PharmacyApp.PostIndex;
using PharmacyApp.Services;

namespace PharmacyApp.Registrations;

public static class PostItRegistration
{
    public static OptionsBuilder<PostItOptions> RegisterPostIt(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPostIndexService, PostIndexService>()
            .AddSingleton<IValidateOptions<PostItOptions>, PostItOptionsValidator>()
            .AddOptions<PostItOptions>();
    }
}