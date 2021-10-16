using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Injection;
using Microsoft.Extensions.DependencyInjection;

namespace ViewAnalyzer.Models
{
    public class DataFactory
    {
        IServiceProvider serviceProvider;
        public DataFactory(InjectBuilder injectBuilder)
        {
            serviceProvider = injectBuilder.ServiceProvider;
        }

        public Database Create()
        {
            return serviceProvider.GetService<Database>();
        }

        public TService GetService<TService>()
        {
            return serviceProvider.GetService<TService>();
        }
    }
}
