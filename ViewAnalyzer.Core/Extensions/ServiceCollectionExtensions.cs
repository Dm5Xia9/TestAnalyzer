using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewAnalyzer.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesFromMarkerAndType<TType>(this IServiceCollection services, Type marker)
        {
            var types = marker.Assembly.GetTypes().Where(p => p.BaseType == typeof(TType));

            foreach (var t in types)
                services.AddTransient(t);

            return services;
        }
    }
}
