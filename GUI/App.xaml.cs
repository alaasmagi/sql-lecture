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
        public static IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            var directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var dbPath = Path.Combine(directory, "uni.db");

            // EF Core
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));
            services.AddScoped<IRepository, EfRepository>();

            // Raw SQL
            services.AddScoped<SqlRepository>(sp =>
                new SqlRepository($"Data Source={dbPath}"));

            Services = services.BuildServiceProvider();

            base.OnStartup(e);
        }
    }
}
