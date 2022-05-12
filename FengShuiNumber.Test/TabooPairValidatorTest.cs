using FengShuiNumber.Dtos;
using FengShuiNumber.Services;
using FengShuiNumber.Services.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiNumber.Test
{
    internal class TabooPairValidatorTest
    {
        private IFengShuiFilter _validator;
        private Mock<IOptionsSnapshot<FengShuiNumberConfiguration>>? _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IOptionsSnapshot<FengShuiNumberConfiguration>>();
            _configuration.Setup(x => x.Value).Returns(new FengShuiNumberConfiguration
            {
                TabooPairNumbers = new List<string>() { "49" }
            });
            _validator = new TabooPairFilter(_configuration.Object);
        }

        [Test]
        public void TabooPairValidator_ShouldResultOnlyMatchedNumber()
        {
            var input = new List<string>
            {
                "0884813449",
                "0884413549",
                "0914813549",
                "0914813549",
                "0916829926",
            };
            var expected = new List<string>
            {
                "0916829926",
            };

            var actual = _validator.DoFilter(new FilterInput { Numbers = input });

            Assert.AreEqual(actual.Count(), expected.Count());
            Assert.IsEmpty(actual.Except(expected));
        }
    }
}
