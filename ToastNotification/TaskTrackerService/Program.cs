using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;


namespace TaskTrackerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Arguments: {string.Join(' ', args)}");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions<TaskWriterOptions>()
                        .Bind(hostContext.Configuration.GetSection(TaskWriterOptions.SectionName))
                        .ValidateDataAnnotations();

                    services.AddHostedService<Worker>();

                    services.AddTransient<ITaskWriter, TaskWriter>();
                    services.AddTransient<ITaskManager, TaskManager>();
                    services.AddTransient<ICommandManager, CommandManager>();
                })
                .UseWindowsService();
    }
}
