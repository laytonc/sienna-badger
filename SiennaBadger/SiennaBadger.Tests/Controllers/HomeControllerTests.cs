using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SiennaBadger.Web.Controllers;
using Xunit;

namespace SiennaBadger.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ShouldReturnView()
        {
            //arrange

            //act
            var sut = new HomeController();
            var response = sut.Index();

            //assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void About_ShouldReturnView()
        {
            //arrange

            //act
            var sut = new HomeController();
            var response = sut.About();

            //assert
            response.Should().NotBeNull();
            response.Should().BeOfType<ViewResult>();
        }

        
    }
}
