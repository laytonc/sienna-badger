using System.Threading.Tasks;
using SiennaBadger.Data.Models;

namespace SiennaBadger.Infrastructure.Services
{
    public interface IParserService
    {
        Task<PageSummary> ParsePageAsync(string url);
    }
}
