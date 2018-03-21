using System.Linq;
using FluentAssertions;
using SiennaBadger.Infrastructure.Extensions;
using Xunit;

namespace SiennaBadger.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void Words_ShouldReturnWordList()
        {
            //arrange
            string content = @"Ullamco pastrami pork belly swine ham. Excepteur ham hock shank pork belly cupidatat doner.";
            
            //act
            var sut = content.Words().ToList();

            //assert
            sut.Should().NotBeNull();

            // confirm distinct words
            sut.Count.Should().Be(11);
            sut.Sum(m => m.Count).Should().Be(14);
        }

        [Fact]
        public void Words_ShouldHandleHyphens()
        {
            //arrange
            string content = @"Ullamco-time pastrami pork belly swine ham. Excepteur ham hock shank pork belly cupidatat doner.";

            //act
            var sut = content.Words().ToList();

            //assert
            sut.Should().NotBeNull();

            // confirm distinct words
            sut.Count.Should().Be(11);
            sut.Sum(m => m.Count).Should().Be(14);
        }

        [Fact]
        public void Words_ShouldHandleApostrophes()
        {
            //arrange
            string content = @"Ullamco we're pastrami pork belly swine ham. Excepteur ham hock shank pork belly cupidatat doner.";

            //act
            var sut = content.Words().ToList();

            //assert
            sut.Should().NotBeNull();

            // confirm distinct words
            sut.Count.Should().Be(12);
            sut.Sum(m => m.Count).Should().Be(15);
        }
    }
}
