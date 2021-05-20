using Microsoft.Extensions.DependencyInjection;
using RochesterConverter.Application;
using RochesterConverter.Application.Interface;
using RochesterConverter.Infrastructure;
using System;
using System.Windows.Forms;

namespace RochesterConverter
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterApplication();
            serviceCollection.RegisterInfrastructure();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm(serviceProvider.GetRequiredService<ICSVFactory>(), serviceProvider.GetRequiredService<IPDFToImageConverterService>(), serviceProvider.GetRequiredService<IValidateService>()));
        }
    }
}
