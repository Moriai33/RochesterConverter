using Microsoft.Extensions.DependencyInjection;
using RochesterConverter.Application.Interface;

namespace RochesterConverter.Application
{
    public static class DependencyInjection
    {
        public static void RegisterApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICSVFactory, CSVFactory>();
            serviceCollection.AddTransient<IPDFToImageConverterService, PDFToImageConverterService>();
            serviceCollection.AddTransient<IValidateService, ValidateService>();
        }
    }
}
