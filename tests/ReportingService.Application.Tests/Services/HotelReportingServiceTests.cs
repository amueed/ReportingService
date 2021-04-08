using Moq;
using NUnit.Framework;
using ReportingService.Application.Common;
using ReportingService.Application.Models;
using ReportingService.Application.Repositories;
using ReportingService.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReportingService.Application.Tests.Reporting
{
    public class HotelReportingServiceTests
    {
        [Test]
        public async Task Should_Generate_Report_File()
        {
            var testData = new HotelData
            {
                Hotel = new Hotel
                {
                    Classification = 5,
                    HotelID = 7294,
                    Name = "Kempinski Bristol Berlin",
                    Reviewscore = 8.3
                },
                HotelRates = new List<HotelRate>
                {
                    new HotelRate
                    {
                        Adults = 2,
                        Los = 1,
                        Price = new Price
                        {
                            Currency = "EUR",
                            NumericFloat = 116.1,
                            NumericInteger = 11610
                        },
                        RateDescription = "Unsere Classic Zimmer",
                        RateID = "123",
                        RateName = "Classic Zimmer",
                        TargetDay = DateTime.Parse("2016-03-15T00:00:00.000+01:00"),
                        RateTags = new List<RateTag>
                        {
                            new RateTag
                            {
                                Name = "breakfast",
                                Shape = false
                            }
                        }
                    }
                }
            };

            //mock repo
            var repository = new Mock<IRepository<HotelData>>();
            repository.Setup(x => x.GetAsync()).ReturnsAsync(testData);

            //mock report generator
            var reportGenerator = new Mock<IReportGenerator>();
            reportGenerator.Setup(x => x.GenerateAsync(It.IsAny<List<HotelReportModel>>())).Verifiable();

            var reportService = new HotelReportingService(repository.Object, reportGenerator.Object);
            await reportService.GenerateReport();

            reportGenerator.Verify();
            Assert.Pass();
        }
    }
}
