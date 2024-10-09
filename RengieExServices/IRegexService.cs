using LanguageExt.Common;
using RengieExModels.Requests;
using RengieExModels.Responses;

namespace RengieExServices;

/// <summary>
/// Service to perform regex operations
/// </summary>
public interface IRegexService
{
    /// <summary>
    /// Check if the text in the request matches the regular expression
    /// </summary>
    /// <param name="request">Contains the text and the pattern to match against.</param>
    /// <returns>Matching information</returns>
    Task<Result<MatchResult>> IsMatch(RegexRequest request);
}