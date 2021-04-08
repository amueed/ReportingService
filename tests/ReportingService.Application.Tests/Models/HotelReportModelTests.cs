using NUnit.Framework;
using ReportingService.Application.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ReportingService.Application.Tests.Models
{
    public class HotelReportModelTests
    {
        private HotelData hotelData;
        [SetUp]
        public void Setup()
        {
            hotelData = new HotelData
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
        }

        [Test]
        public void Should_Map_Hotel_Data_Into_List_of_Hotel_Model()
        {
            var mapped = HotelReportModel.Map(hotelData);

            Assert.IsInstanceOf<List<HotelReportModel>>(mapped);
            Assert.IsNotNull(mapped);
            Assert.AreEqual(1, mapped.Count);
        }

        [Test]
        public void Should_Map_Hotel_Data_Breakfast_Property()
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

            var actual = HotelReportModel.Map(hotelData);
            var actualValue = actual[0].BreakfastIncluded;
            var expectedValue = 0;

            Assert.IsInstanceOf<List<HotelReportModel>>(actual);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void Should_Map_Hotel_Data_DepartureDate_Property()
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

            var actual = HotelReportModel.Map(hotelData);
            var actualValue = actual[0].DepartureDate;

            var hotelRate = hotelData.HotelRates[0];
            var expectedValue = hotelRate.TargetDay.AddDays(hotelRate.Los).ToString("dd.MM.yyyy");

            Assert.IsInstanceOf<List<HotelReportModel>>(actual);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
