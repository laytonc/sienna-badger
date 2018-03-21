using SiennaBadger.Web.Controllers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SiennaBadger.Data.Models;
using SiennaBadger.Infrastructure.Services;
using Xunit;

namespace SiennaBadger.Tests.Controllers
{
    public class PageControllerTests
    {
        [Fact]
        public async Task Parse_ShouldReturnPageSummary()
        {
            //arrange
            var logger = Substitute.For<ILogger<PageController>>();
            var parserService = Substitute.For<IParserService>();
            var url = "https://localhost";
            var pageSummary = new PageSummary();

            parserService.ParsePageAsync(url).Returns(Task.FromResult(pageSummary));

            //act
            var sut = new PageController(parserService,logger);
            var response = await sut.Parse(url) as OkObjectResult;
            //assert
            response.Should().NotBeNull();
            if (response != null)
            {
                response.Value.Should().NotBeNull();
                response.Value.Should().BeOfType<PageSummary>();
                response.Value.Should().Be(pageSummary);
            }
        }
    }
}
