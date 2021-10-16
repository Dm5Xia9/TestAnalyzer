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

            var startup = new Startup();

            startup.ServiceConfigure(builder.Build());

            startup.Configure(builder.GetServiceProvider());


        }
    }
}
