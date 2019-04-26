using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Api.Template.Domain.Tests.Models
{
    [TestFixture]
    public class TemplateDomainUnitTest
    {
        [TestCase(true)]
        [TestCase(false)]
        public async Task WhenInputEqualsTrueThenSucess(bool value)
        {
            //arrange
            var currentValue = false;
            var expectedResult = value;

            //act
            currentValue = await CalculateValue(value);

            //assert
            currentValue.Should()
                .Be(expectedResult);
        }

        private Task<bool> CalculateValue()
        {
            return Task.Run(() => CalculateValue(true));
        }

        private Task<bool> CalculateValue(bool value)
        {
            return Task.Run(() => value);
        }

        [Test]
        public async Task WhenConditionThenResult()
        {
            //arrange
            var currentValue = false;
            var expectedResult = true;

            //act
            currentValue = await CalculateValue();

            //assert
            currentValue.Should()
                .Be(expectedResult);
        }
    }
}