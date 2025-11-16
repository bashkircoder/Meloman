using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using Music.ExternalSystems.Cloudinary;
using Music.Cloudinary;

namespace Music.ExternalSystems.Cloudinary
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(
                configuration.GetSection("Cloudinary"));

            services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;

                var account = new Account(
                    config.CloudName,
                    config.ApiKey,
                    config.ApiSecret
                );

                return new CloudinaryDotNet.Cloudinary(account);
            });

            services.AddScoped<ICloudinaryUploader, CloudinaryUploader>();

            return services;
        }
    }
}
