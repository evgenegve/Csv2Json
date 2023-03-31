using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Csv2Json.MVC.Models;
using Csv2Json.Services;

namespace Csv2Json.MVC.Controllers;

public class TestController : Controller
{
    private readonly ILogger<TestController> _logger;
    private readonly ICsv2JsonTransform _csv2JsonTransform;

    public TestController(ILogger<TestController> logger, ICsv2JsonTransform csv2JsonTransform)
    {
        _logger = logger;
        _csv2JsonTransform = csv2JsonTransform;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public JsonResult Transform() =>  Json(_csv2JsonTransform.Transform("id,Name,Age/n1,Evgeny,36/n2,Andrey,38/n3,Alexander,27"));
    [HttpPost]
    public JsonResult Transform1(string csv) =>  Json(_csv2JsonTransform.Transform(csv));

}