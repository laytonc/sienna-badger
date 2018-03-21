using Microsoft.Extensions.Logging;
using NSubstitute;
using SiennaBadger.Infrastructure.Services;
using Xunit;

namespace SiennaBadger.Tests.Services
{
    public class ParserServiceTests
    {
        [Fact]
        public void ParsePageAsync_ShouldReturnPageSummary()
        {
            // arrange
            var logger = Substitute.For<ILogger<ParserService>>();

            // act
            var sut = new ParserService(logger);

            // assert

        }
    }
}
