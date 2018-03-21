using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SiennaBadger.Infrastructure.Services;
using Xunit;

namespace SiennaBadger.Tests.Services
{
    public class ParserServiceTests
    {
        [Fact]
        public async Task ParsePageAsync_ShouldReturnPageSummary()
        {
            // arrange
            var logger = Substitute.For<ILogger<ParserService>>();
            var url = "https://wahoo-siennabadger-dev.azurewebsites.net/html/test-basic.html";

            // act
            var sut = new ParserService(logger);
            //var result = await sut.ParsePageAsync(url);

            // assert

        }
    }
}
