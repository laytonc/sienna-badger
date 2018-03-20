using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SiennaBadger.Data.Models;

namespace SiennaBadger.Infrastructure.Services
{
    public interface IParserService
    {
        Task<PageSummary> ParsePageAsync(string url);
    }
}
