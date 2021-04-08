using ReportingService.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReportingService.Application.Services
{
    public interface IReportGenerator
    {
        Task<string> GenerateAsync(List<HotelReportModel> data);
    }
}
