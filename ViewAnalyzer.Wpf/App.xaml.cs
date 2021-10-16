using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using ViewAnalyzer.Core.Injection;


namespace ViewAnalyzer.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var builder = new InjectBuilder();


            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var startup = new Startup(config.Build());

            startup.ServiceConfigure(builder.Build());

            startup.Configure(builder.GetServiceProvider());


        }
    }
}
