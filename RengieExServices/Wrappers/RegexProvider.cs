using System.Text.RegularExpressions;

namespace RengieExServices.Wrappers
{
    /// <summary>
    /// Interface for regex operations
    /// </summary>
    public interface IRegexProvider
    {
        /// <summary>
        /// Creates a regular expression object for the specified regular expression, with options that modify the pattern.
        /// </summary>
        Regex CreateRegex(string pattern, RegexOptions options = RegexOptions.Compiled);

        /// <summary>
        /// Returns all the successful matches as if Match were called iteratively numerous times.
        /// </summary>
        MatchCollection Matches(string input, string pattern, RegexOptions options);

        /// <summary>
        /// Replaces all occurrences of
        /// the <paramref name="pattern "/>with the <paramref name="replacement "/>
        /// pattern, starting at the first character in the input string.
        /// </summary>
        string Replace(string input, string pattern, string substitutionPattern, RegexOptions options);
    }

    /// <summary>
    /// Implementation of the regex operations that wrap most used.NET Regex methods
    /// which made it dependency injectable
    /// </summary>
    public class RegexProvider : IRegexProvider
    {
        /// <summary>
        /// Creates a regular expression object for the specified regular expression, with options that modify the pattern.
        /// </summary>
        public Regex CreateRegex(string pattern, RegexOptions options = RegexOptions.Compiled)
        {
            return new Regex(pattern, options);
        }

        /// <summary>
        /// Returns all the successful matches as if Match were called iteratively numerous times.
        /// </summary>
        public MatchCollection Matches(string input, string pattern, RegexOptions options)
        {
            return Regex.Matches(input, pattern, options);
        }


        /// <summary>
        /// Replaces all occurrences of
        /// the <paramref name="pattern "/>with the <paramref name="replacement "/>
        /// pattern, starting at the first character in the input string.
        /// </summary>
        public string Replace(string input, string pattern, string replacement, RegexOptions options)
        {
            return Regex.Replace(input, pattern, replacement, options);
        }
    }
}
