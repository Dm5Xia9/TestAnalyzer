using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewAnalyzer.Core.Injection
{
    public class InjectBuilder
    {
        IServiceCollection serviceCollection;
        public IServiceProvider ServiceProvider { get; set; }
        public IServiceCollection Build()
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(p => this);
            return serviceCollection;
        }

        public IServiceProvider GetServiceProvider()
        {
            ServiceProvider = serviceCollection.BuildServiceProvider();
            return ServiceProvider;
        }


    }
}
