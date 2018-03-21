using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SiennaBadger.Data.Models;
using SiennaBadger.Infrastructure.Services;
using Xunit;

namespace SiennaBadger.Tests.Services
{
    public class ParserServiceTests
    {
        // Note, these are integration tests, for unit tests additional refactoring is required to abstract
        // some pieces of the AngleSharp BrowsingContext

        [Fact]
        public async Task ParsePageAsync_ShouldReturnPageSummary()
        {
            // arrange
            var logger = Substitute.For<ILogger<ParserService>>();
            var url = "https://wahoo-siennabadger-dev.azurewebsites.net/html/test-basic.html";

            // act
            var sut = new ParserService(logger);
            var result = await sut.ParsePageAsync(url);

            // assert
            // basic checks that we are getting a valid page summary object
            result.Should().NotBeNull();
            result.Should().BeOfType<PageSummary>();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.WordCount.Should().BeGreaterThan(0);
            result.Words.Count().Should().BeGreaterThan(0);
            result.Images.Count().Should().BeGreaterThan(0);
        }
    }
}
