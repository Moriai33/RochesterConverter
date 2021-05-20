using Microsoft.Extensions.DependencyInjection;
using RochesterConverter.Application.Interface;

namespace RochesterConverter.Infrastructure
{
    public static class DependencyInjection
    {
        public static void RegisterInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFileOperations, FileOperations>();
            serviceCollection.AddTransient<IImageProcesser, ImageProcesser>();
        }
    }
}
