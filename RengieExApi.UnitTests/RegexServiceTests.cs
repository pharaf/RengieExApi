using System.Text.RegularExpressions;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using RengieExApi.Validators;
using RengieExModels.Responses;
using RengieExServices;
using NFluent;
using RengieExApi.Examples;
using RengieExServices.Wrappers;

namespace RengieExApi.UnitTests
{
    public class RegexServiceTests
    {
        private readonly RegexRequestValidator _regexRequestValidator = new();
        private readonly ILogger<RegexService> _fakeRegexServiceLogger = A.Fake<ILogger<RegexService>>();
        private readonly IRegexProvider _regexProvider = new RegexProvider();

        [Fact]
        public async Task Should_succeeded_and_match()
        {
            var service = new RegexService(_regexRequestValidator, _regexProvider, _fakeRegexServiceLogger);

            var regexRequest = new RegexRequestExample().GetExamples();
            var aggregatedResult = await service.IsMatch(regexRequest);

            var (succeeded, failed) = aggregatedResult.Match<(MatchResult? Succeeded, Exception? Failed)>(
                success => (success, null),
                failure => (null, failure)
            );

            Check.That(succeeded).IsNotNull();
            Check.That(failed).IsNull();
            Check.That(succeeded!.IsMatch).IsTrue();
            Check.That(succeeded.TransformedText).Contains("06-");
        }

        [Fact]
        public async Task Should_succeeded_and_match_when_substitution_is_null()
        {
            var service = new RegexService(_regexRequestValidator, _regexProvider, _fakeRegexServiceLogger);

            var regexRequest = new RegexRequestExample().GetExamples();
            regexRequest.SubstitutionPattern = null!;
            var aggregatedResult = await service.IsMatch(regexRequest);

            var (succeeded, failed) = aggregatedResult.Match<(MatchResult? Succeeded, Exception? Failed)>(
                success => (success, null),
                failure => (null, failure)
            );

            Check.That(succeeded).IsNotNull();
            Check.That(failed).IsNull();
            Check.That(succeeded!.IsMatch).IsTrue();
            Check.That(succeeded.TransformedText).DoesNotContain("06");
        }

        [Fact]
        public async Task Should_succeeded_but_not_match()
        {
            var service = new RegexService(_regexRequestValidator, _regexProvider, _fakeRegexServiceLogger);

            var regexRequest = new RegexRequestExample().GetExamples();

            regexRequest.Text = "Some text";

            var aggregatedResult = await service.IsMatch(regexRequest);

            var (succeeded, failed) = aggregatedResult.Match<(MatchResult? Succeeded, Exception? Failed)>(
                success => (success, null),
                failure => (null, failure)
            );

            Check.That(succeeded).IsNotNull();
            Check.That(failed).IsNull();
            Check.That(succeeded!.IsMatch).IsFalse();
        }

        [Theory]
        [InlineData("Some text", "", RegexOptions.None, "0$2-$3-$4-$5-$6")]
        [InlineData("Some text", null, RegexOptions.None, "0$2-$3-$4-$5-$6")]
        [InlineData("", "yo", RegexOptions.None, "0$2-$3-$4-$5-$6")]
        [InlineData(null, "yo", RegexOptions.None, "0$2-$3-$4-$5-$6")]
        public async Task Should_send_exception_when_pattern_or_text_is_null(string text, string pattern, RegexOptions options, string substitutionPattern)
        {
            var service = new RegexService(_regexRequestValidator, _regexProvider, _fakeRegexServiceLogger);

            var regexRequest = new RegexRequestExample().GetExamples();
            regexRequest.Text = text;
            regexRequest.Pattern = pattern;
            regexRequest.Options = options;
            regexRequest.SubstitutionPattern = substitutionPattern;

            var aggregatedResult = await service.IsMatch(regexRequest);

            var (succeeded, failed) = aggregatedResult.Match<(MatchResult? Succeeded, Exception? Failed)>(
                success => (success, null),
                failure => (null, failure)
            );

            Check.That(succeeded).IsNull();
            Check.That(failed).IsNotNull();
        }
    }
}