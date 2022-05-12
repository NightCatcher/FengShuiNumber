using FengShuiNumber.Dtos;
using FengShuiNumber.FengshuiFilter;
using FengShuiNumber.FengshuiFilter.Interfaces;
using FengShuiNumber.Services;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FengShuiNumber.Test
{
    public class FengshuiFilterComposerTest
    {
        private Mock<IOptionsSnapshot<FengShuiNumberConfiguration>>? _configuration;
        private IFengShuiFilterComposer _composer;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IOptionsSnapshot<FengShuiNumberConfiguration>>();
            _configuration.Setup(x => x.Value).Returns(new FengShuiNumberConfiguration
            {
                NumberLengthLimit = 10,
                FengShuiRate = new List<string>() { "24/28" },
                NicePairNumbers = new List<string>() { "26" },
                HeadNumbers = new Dictionary<string, IEnumerable<string>>
                {
                    { "Viettel", new List<string> { "086", "096", "097"  } },
                    { "Vinaphone", new List<string> { "089", "090", "093" } },
                    { "Mobifone", new List<string> { "088", "091", "094"  } },
                },
                TabooPairNumbers = new List<string>() { "49" }
            });
            _composer = new FengShuiFilterComposer();
            _composer.AddFilter(new FengShuiRateFilter(_configuration.Object));
            _composer.AddFilter(new NiceLastPairFilter(_configuration.Object));
            _composer.AddFilter(new TabooPairFilter(_configuration.Object));
            _composer.AddFilter(new FengshuiLengthFilter(_configuration.Object));
            _composer.AddFilter(new HeaderNumberFilter(_configuration.Object));
        }

        [Test]
        public void FengShuiRateValidator_ShouldResultOnlyMatchedNumber()
        {
            var input = new List<string>
            {
                "08848135499",
                "0884813549",
                "0914813549",
                "0914813549",
                "0916829926",
            };
            var expected = new List<string>
            {
                "0916829926"
            };

            var actual = _composer.Filter(new FilterInput { Numbers = input, NetworkCarrier = "Mobifone" });

            Assert.AreEqual(actual.Count(), expected.Count());
            Assert.IsEmpty(actual.Except(expected));
        }
    }
}
