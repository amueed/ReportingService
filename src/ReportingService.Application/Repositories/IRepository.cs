using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReportingService.Application.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetAsync();
    }
}
