using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewAnalyzer.Api;
using ViewAnalyzer.Core.Data;
using ViewAnalyzer.Core.Extensions;
using ViewAnalyzer.Models;
using ViewAnalyzer.Wpf.Services;

namespace ViewAnalyzer.Wpf
{
    public class Startup
    {
        public Startup()
        {
        }

        public void ServiceConfigure(IServiceCollection serviceCollection)
        {
            //TODO: убрать все базовые сервисы в отдельное место 
            serviceCollection.AddSingleton<NavigationManager>();

            serviceCollection.AddServicesFromMarkerAndType<Window>(typeof(Startup));

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql("*");

            serviceCollection.AddTransient<AppDbContext>(p => new AppDbContext(optionsBuilder.Options));

            serviceCollection.AddTransient<UserRoleManager>();
            serviceCollection.AddTransient<Database>();
            serviceCollection.AddSingleton<DataFactory>();

            serviceCollection.AddSingleton<AnalyzerService>(p =>
                new AnalyzerService("localhost", "5000"));

            serviceCollection.AddTransient<AnalyzerProcessor>();
        }

        public void Configure(IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<NavigationManager>().Show<SignIn>();
        }
    }
}
