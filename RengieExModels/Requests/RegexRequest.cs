using System.Text.RegularExpressions;

namespace RengieExModels.Requests;

/// <summary>
/// Request for regex operations
/// </summary>
public class RegexRequest
{
    /// <summary>
    /// The pattern to match example: (\+33|0) ?(\d) ?(\d\d) ?(\d\d) ?(\d\d) ?(\d\d)
    /// </summary>
        
    public string Pattern { get; set; } = string.Empty;
    /// <summary>
    /// The text to check. Example: My phone is 06 12 23 45 56.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Flags to use with the regex. Example: RegexOptions.IgnoreCase | RegexOptions.Multiline
    /// </summary>
    public RegexOptions Options { get; set; } = RegexOptions.None; // Par défaut pas d'options

    /// <summary>
    /// The pattern to use for substitution. Example: 0$2-$3-$4-$5-$6
    /// </summary>
    public string? SubstitutionPattern { get; set; } = "";
}