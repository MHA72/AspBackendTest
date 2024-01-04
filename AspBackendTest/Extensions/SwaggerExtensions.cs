using Microsoft.OpenApi.Models;

namespace AspBackendTest.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "LoginJWT",
                Contact = new OpenApiContact
                {
                    Name = "Mohammad Hossein Arabbagheri",
                    Email = "hossinarab727@gmail.com"
                }
            });
        });
        return serviceCollection;
    }
}