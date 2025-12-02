using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.IO;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            var directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var dbPath = Path.Combine(directory, "uni.db");

            // EF setup
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            // Uncomment this to use EF
            services.AddScoped<IRepository, EfRepository>();

            // Uncomment this to use raw SQL
            /*services.AddScoped<IRepository, SqlRepository>(sp =>
                new SqlRepository($"Data Source={dbPath}"));*/

            Services = services.BuildServiceProvider();

            base.OnStartup(e);
        }
    }
}
