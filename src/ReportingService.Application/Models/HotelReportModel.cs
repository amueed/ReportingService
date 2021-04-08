using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ReportingService.Application.Models
{
    public class HotelReportModel
    {
        [Description("ARRIVAL_DATE")]
        public string ArrivalDate { get; set; }

        [Description("DEPARTURE_DATE")]
        public string DepartureDate { get; set; }

        [Description("PRICE")]
        public Double Price { get; set; }

        [Description("CURRENCEY")]
        public string Currency { get; set; }

        [Description("RATENAME")]
        public string RateName { get; set; }

        [Description("ADULTS")]
        public int Adults { get; set; }

        [Description("BREAKFAST_INCLUDED")]
        public int BreakfastIncluded { get; set; }

        public static List<HotelReportModel> Map(HotelData data)
        {
            return data.HotelRates.Select(x => new HotelReportModel
            {
                ArrivalDate = x.TargetDay.ToString("dd.MM.yyyy"),
                DepartureDate = x.TargetDay.AddDays(x.Los).ToString("dd.MM.yyyy"),
                Price = x.Price.NumericFloat,
                Currency = x.Price.Currency,
                RateName = x.RateName,
                Adults = x.Adults,
                BreakfastIncluded = x.RateTags.Any(x => x.Name == "breakfast" && x.Shape) ? 1 : 0

            }).ToList();
        }
    }
}
