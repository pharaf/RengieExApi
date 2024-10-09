using Microsoft.AspNetCore.Mvc;
using RengieExModels.Requests;
using RengieExServices;

namespace RengieExApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegexController(IRegexService regexService) : ControllerBase
{
    [HttpPost("match")]
    public async Task<IActionResult> IsMatch([FromBody] RegexRequest request)
    {
        var result = await regexService.IsMatch(request);

        //result is a monad, we need to match it to get the value
        return result.Match<IActionResult>(
            success => Ok(success),
            failure => BadRequest(failure)
        );
    }
}