using ReportingService.Application.Common;
using ReportingService.Application.Models;
using ReportingService.Application.Repositories;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReportingService.Infrasturcture.Repositories
{
    public class HotelRepository : IRepository<HotelData>
    {
        readonly IFileClient _fileClient;
        public HotelRepository(IFileClient fileClient)
        {
            _fileClient = fileClient;

        }
        public async Task<HotelData> GetAsync()
        {
            using (var fileStream = await _fileClient.GetFile("App_Data", "hotelrates.json"))
            {
                var rawJson = new StreamReader(fileStream).ReadToEnd();
                return JsonSerializer.Deserialize<HotelData>(rawJson);
            }
        }
    }
}
