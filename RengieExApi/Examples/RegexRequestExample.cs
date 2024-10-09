using RengieExModels.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace RengieExApi.Examples;

/// <summary>
/// Example for the RegexRequest used by Swagger
/// </summary>
public class RegexRequestExample : IExamplesProvider<RegexRequest>
{
    public RegexRequest GetExamples()
    {
        return new RegexRequest
        {
            Pattern = "(\\+33|0) ?(\\d) ?(\\d\\d) ?(\\d\\d) ?(\\d\\d) ?(\\d\\d)",
            Text = "My phone is 06 12 23 45 56. Can you confirm that your phone is +33 6 11 22 33 44? I think that 0123456789 is the best phone number.",
            Options = 0,
            SubstitutionPattern = "0$2-$3-$4-$5-$6"
        };
    }
}