namespace RengieExModels.Responses;

/// <summary>
/// Result of a regex match
/// </summary>
public class MatchResult
{
    /// <summary>
    /// Indicates if the text matches the pattern
    /// </summary>
    public bool IsMatch { get; set; }

    /// <summary>
    /// Transformed text after applying the substitution pattern
    /// </summary>
    public string TransformedText { get; set; } = string.Empty;

    /// <summary>
    /// Information about the matches
    /// </summary>
    public IEnumerable<MatchingInformation> MatchingInformation { get; set; } = [];
}

/// <summary>
/// Information a group of matches
/// </summary>
public class MatchingInformation : List<MatchDetails>;

/// <summary>
/// Match details for a group or the full match
/// </summary>
public class MatchDetails(string label, int start, int length, string value)
{
    /// <summary>
    /// Label for the match
    /// </summary>
    public string Label { get; set; } = label;

    ///// <summary>
    ///// Range of the match
    ///// </summary>
    ////public RegexRange Range { get; set; } = range;


    /// <summary>
    /// Start position of the match
    /// </summary>
    public int Start { get; set; } = start;

    /// <summary>
    /// End position of the match
    /// </summary>
    public int End { get; set; } = start + length;

    /// <summary>
    /// Value of the match
    /// </summary>
    public string Value { get; set; } = value;
}
