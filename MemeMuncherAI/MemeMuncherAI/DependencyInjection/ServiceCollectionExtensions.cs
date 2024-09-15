using MemeMuncherAI.Util;
using MemeMuncherAI.Util.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace MemeMuncherAI.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMemeMuncherServices(this IServiceCollection services)
    {
        services.AddTransient<IDirectoryUtil, DirectoryUtil>();
        services.AddTransient<IFileUtil, FileUtil>();
        services.AddTransient<IImageAnalyzerUtil, ImageAnalyzerUtil>();

        return services;
    }
}
