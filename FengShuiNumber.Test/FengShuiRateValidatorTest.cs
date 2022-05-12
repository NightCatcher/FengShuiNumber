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
    public class FengShuiRateValidatorTest
    {
        private IFengShuiFilter _validator;
        private Mock<IOptionsSnapshot<FengShuiNumberConfiguration>>? _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IOptionsSnapshot<FengShuiNumberConfiguration>>();
            _configuration.Setup(x => x.Value).Returns(new FengShuiNumberConfiguration
            {
                FengShuiRate = new List<string>() { "24/28" }
            });
            _validator = new FengShuiRateFilter(_configuration.Object);
        }

        [Test]
        public void FengShuiRateValidator_ShouldResultOnlyMatchedNumber()
        {
            var input = new List<string>
            {
                "0884813549",
                "0884813549",
                "0914813549",
                "0914813549",
                "0916829926",
            };
            var expected = new List<string>
            {
                "0916829926"
            };

            var actual = _validator.DoFilter(new FilterInput { Numbers = input});

            Assert.AreEqual(actual.Count(), expected.Count());
            Assert.IsEmpty(actual.Except(expected));
        }
    }
}