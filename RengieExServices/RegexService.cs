using RengieExModels.Requests;
using FluentValidation;
using RengieExModels.Responses;
using System.Text.RegularExpressions;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using RengieExServices.Wrappers;

namespace RengieExServices
{
    /// <summary>
    /// Service to perform regex operations
    /// </summary>
    public class RegexService(
        IValidator<RegexRequest> validator,
        IRegexProvider regexProvider,
        ILogger<RegexService> logger)
        : IRegexService
    {
        /// <summary>
        /// Check if the text in the request matches the regular expression
        /// </summary>
        /// <param name="request">Contains the text and the pattern to match against.</param>
        /// <returns>Matching information</returns>
        public async Task<Result<MatchResult>> IsMatch(RegexRequest request)
        {
            logger.LogInformation(
                "A match was requested with.\n Pattern : {pattern}\n Text: {text}\n SubstitutionPattern: {SubstitutionPattern}\n Option: {option}",
                request.Pattern, request.Text, request.SubstitutionPattern, request.Options);

            // Validate the request input
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                logger.LogError("The pattern and the text must not be null or empty");
                return new Result<MatchResult>(new ValidationException(validationResult.Errors));
            }

            var regex = regexProvider.CreateRegex(request.Pattern, request.Options);
            var matchResult = new MatchResult
            {
                IsMatch = regex.IsMatch(request.Text),
                TransformedText = request.Text
            };

            if (matchResult.IsMatch)
            {
                //replace the text with the substitution pattern
                matchResult.TransformedText = regex.Replace(request.Text, request.SubstitutionPattern ?? "");

                //get the matching information
                matchResult.MatchingInformation = GetMatchingInformationCollection(request, regex);
            }

            return new Result<MatchResult>(matchResult);
        }

        /// <summary>
        /// Get the matching information collection
        /// </summary>
        private static IEnumerable<MatchingInformation> GetMatchingInformationCollection(RegexRequest request, Regex regex)
        {
            var matchCollection = regex.Matches(request.Text);

            foreach (Match match in matchCollection)
            {
                var matchingInformation = new MatchingInformation();
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    var childMatch = match.Groups[i];
                    var childMatchingInformation = new MatchDetails(i == 0 ? $"Full match" : $"Group {i}",
                        childMatch.Index, childMatch.Length, childMatch.Value);
                    matchingInformation.Add(childMatchingInformation);
                }
                yield return matchingInformation;
            }
        }
    }
}
