using Microsoft.Extensions.DependencyInjection;
using ReportingService.Application.Common;
using ReportingService.Application.Models;
using ReportingService.Application.Repositories;
using ReportingService.Application.Services;
using ReportingService.Infrasturcture.Common;
using ReportingService.Infrasturcture.Repositories;
using ReportingService.Infrasturcture.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ReportingService.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //setup DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRepository<HotelData>, HotelRepository>()
                .AddSingleton<IFileClient, LocalFileClient>(client =>
                {
                    var rootPath = Directory.GetCurrentDirectory();
                    return new LocalFileClient(rootPath);
                })
                .AddSingleton<IReportGenerator, ExcelReportGenerator>()
                .AddTransient<HotelReportingService>()
                .BuildServiceProvider();

            //report generation
            var hotelReportingService = serviceProvider.GetService<HotelReportingService>();
            var result = await hotelReportingService.GenerateReport();
            Console.WriteLine($"Report is generated at path: {Directory.GetCurrentDirectory()}\\{result}");
            Console.ReadKey();
        }
    }
}
