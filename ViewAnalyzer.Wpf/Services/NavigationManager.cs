using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewAnalyzer.Core.Injection;
using Microsoft.Extensions.DependencyInjection;

namespace ViewAnalyzer.Wpf.Services
{
    public class NavigationManager
    {
        IServiceProvider serviceProvider;
        public NavigationManager(InjectBuilder injectBuilder)
        {
            this.serviceProvider = injectBuilder.ServiceProvider;
        }

        public Window Show<TWindow>() where TWindow: Window
        {
            var window = serviceProvider.GetService<TWindow>();

            window.Show();

            return window;
        }
    }
}
