using ReportingService.Application.Models;
using ReportingService.Application.Repositories;
using ReportingService.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReportingService.Application.Services
{
    public class HotelReportingService
    {
        private readonly IRepository<HotelData> _repository;
        private readonly IReportGenerator _reportGenerator;

        public HotelReportingService(IRepository<HotelData> repository, IReportGenerator reportGenerator)
        {
            this._repository = repository;
            this._reportGenerator = reportGenerator;
        }
        public async Task<string> GenerateReport()
        {
            var data = await _repository.GetAsync();
            var reportingData = HotelReportModel.Map(data);
            return await _reportGenerator.GenerateAsync(reportingData);
        }
    }
}
