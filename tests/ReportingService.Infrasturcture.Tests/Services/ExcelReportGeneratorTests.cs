using Moq;
using NUnit.Framework;
using ReportingService.Application.Common;
using ReportingService.Application.Models;
using ReportingService.Infrasturcture.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReportingService.Infrasturcture.Tests.Services
{
    public class ExcelReportGeneratorTests
    {
        [Test]
        public async Task Should_Generate_And_Save_Report()
        {
            var sampleData = new List<HotelReportModel> {
                new HotelReportModel
                {
                    Adults = 1,
                    BreakfastIncluded = 1,
                    Currency = "EUR",
                    Price = 250,
                    RateName = "test",
                    ArrivalDate = "12.01.2020",
                    DepartureDate = "14.01.2020"
                },
                new HotelReportModel
                {
                    Adults = 2,
                    BreakfastIncluded = 0,
                    Currency = "EUR",
                    Price = 198.3,
                    RateName = "Awesome",
                    ArrivalDate = "12.01.2020",
                    DepartureDate = "14.01.2020"
                }
            };

            var mockedFileClient = new Mock<IFileClient>();
            var executeDir = Directory.GetCurrentDirectory();
            mockedFileClient.Setup(x => x.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
            .Callback<string, string, Stream>((storeName, filePath, fileStream) =>
            {
                var path = Path.Combine(executeDir, storeName);
                var fullPath = Path.Combine(path, filePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                using (var file = new FileStream(fullPath, FileMode.CreateNew))
                {
                    fileStream.CopyTo(file);
                }
            });

            var generator = new ExcelReportGenerator(mockedFileClient.Object);
            var expected = await generator.GenerateAsync(sampleData);

            var expectedFilePath = $"{executeDir}\\{expected}";

            Assert.IsNotNull(expected);
            Assert.IsTrue(File.Exists(expectedFilePath));
        }
    }
}
