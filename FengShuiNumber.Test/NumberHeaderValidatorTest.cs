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
    internal class NumberHeaderValidatorTest
    {
        private IFengShuiFilter _validator;
        private Mock<IOptionsSnapshot<FengShuiNumberConfiguration>>? _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IOptionsSnapshot<FengShuiNumberConfiguration>>();
            _configuration.Setup(x => x.Value).Returns(new FengShuiNumberConfiguration
            {
                HeadNumbers = new Dictionary<string, IEnumerable<string>>
                {
                    { "Viettel", new List<string> { "086", "096", "097"  } },
                    { "Vinaphone", new List<string> { "089", "090", "093" } },
                    { "Mobifone", new List<string> { "088", "091", "094"  } },
                }
            });
            _validator = new HeaderNumberFilter(_configuration.Object);
        }

        [Test]
        public void NumberHeaderValidator_ShouldResultOnlyMatchedNumber()
        {
            var input = new List<string>
            {
                "0874763549",
                "0874813549",
                "0914813549",
                "0926829926",
            };
            var expected = new List<string>
            {
                "0914813549",
            };
            var actual = _validator.DoFilter(new FilterInput { Numbers = input, NetworkCarrier = "Mobifone" });

            Assert.AreEqual(actual.Count(), expected.Count());
            Assert.IsEmpty(actual.Except(expected));
        }
    }
}
