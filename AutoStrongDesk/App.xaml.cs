using AutoStrongDesk.Interfaces;
using AutoStrongDesk.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;

namespace AutoStrongDesk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; }

        public new static App Current => (App)Application.Current;


        public App()
        {
            Services = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddRefitClient<IPostsClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]!));
            services.AddSingleton<IPostService, PostService>();

            return services.BuildServiceProvider();
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
            e.Handled = true;
        }
    }

}
