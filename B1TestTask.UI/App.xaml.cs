using B1TestTask.UI.Data;
using B1TestTask.UI.Data.Readers;
using B1TestTask.UI.Data.Readers.Base;
using B1TestTask.UI.Data.Repositories;
using B1TestTask.UI.Data.Repositories.Base;
using B1TestTask.UI.Models;
using B1TestTask.UI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace B1TestTask.UI;
public partial class App
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<DataContext>(options =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString);
                });

                services.AddDbContext<BankContext>(options =>
                {
                    var connectionString = context.Configuration.GetConnectionString("BankConnection");
                    options.UseSqlServer(connectionString);
                });

                services.AddScoped(typeof(IRepository<>), typeof(BankRepository<>));
                services.AddScoped<IRepository<RandomRowData>, EfRepository<RandomRowData>>();

                services.AddScoped<IExcelReader<BankAccountClass>, ExcelReader>();

                services.AddSingleton<MainWindowViewModel>();

                services.AddSingleton((services) => new MainWindow()
                {
                    DataContext = services.GetRequiredService<MainWindowViewModel>()
                });
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
}

