using Microsoft.AspNetCore.Mvc;
using Csv2Json.Services;

namespace Csv2Json.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class Csv2JsonTransformController : ControllerBase
{
    private readonly ILogger<Csv2JsonTransformController> _logger;
    private readonly ICsv2JsonTransform _csv2JsonTransform;

    public Csv2JsonTransformController(ILogger<Csv2JsonTransformController> logger, ICsv2JsonTransform csv2JsonTransform)
    {
        _logger = logger;
        _csv2JsonTransform = csv2JsonTransform;

    }

    [HttpGet(Name = "Transform")]    
    public ActionResult<string> Transform(string csv)
    {
        try
        { 
            return Ok(_csv2JsonTransform.Transform(csv));
        }
        catch (FormatException e)
        {
            return BadRequest(e.Message);
        }
    }
}
