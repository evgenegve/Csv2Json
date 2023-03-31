using Microsoft.AspNetCore.Mvc;
using Csv2Json.Services;

namespace Csv2Json.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet(Name = "Transform1")]    
    public ActionResult<TestModel> GetTest(string csv)
    {
        try
        { 
            return Ok(new TestModel(csv, 1));
        }
        catch (FormatException e)
        {
            return BadRequest(e.Message);
        }
    }
}

public class TestModel
{
    public string F1 {get;set;}
    public int F2 {get;set;}
    public TestModel(string f1, int f2Value) => (F1, F2) = (f1, f2Value);
}
