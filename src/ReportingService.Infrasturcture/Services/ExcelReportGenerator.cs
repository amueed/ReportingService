using OfficeOpenXml;
using OfficeOpenXml.Table;
using ReportingService.Application.Common;
using ReportingService.Application.Models;
using ReportingService.Application.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ReportingService.Infrasturcture.Services
{
    public class ExcelReportGenerator : IReportGenerator
    {
        private readonly IFileClient _fileClient;
        public ExcelReportGenerator(IFileClient fileClient)
        {
            this._fileClient = fileClient;
        }
        public async Task<string> GenerateAsync(List<HotelReportModel> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var p = new ExcelPackage())
            {
                //A workbook must have at least on cell, so lets add one... 
                var ws = p.Workbook.Worksheets.Add("Sheet1");

                //write data to excel
                ws.Cells["A1"].LoadFromCollection(data, true, TableStyles.Light6);
                ws.Cells.AutoFitColumns();

                //save file
                var storeName = "App_Data";
                var filePath = $"{Guid.NewGuid()}.xlsx";
                await _fileClient.SaveFile(storeName, filePath, new MemoryStream(await p.GetAsByteArrayAsync()));
                return $"{storeName}\\{filePath}";
            }
        }
    }
}
