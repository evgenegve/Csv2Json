using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Csv2Json.MVC.Models;
using Csv2Json.Services;
using System;

namespace Csv2Json.MVC.Controllers;

public class TransformController : Controller
{
    private readonly ILogger<TransformController> _logger;
    private readonly ICsv2JsonTransform _csv2JsonTransform;

    public TransformController(ILogger<TransformController> logger, ICsv2JsonTransform csv2JsonTransform)
    {
        _logger = logger;
        _csv2JsonTransform = csv2JsonTransform;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public JsonResult Transform(string csv) 
    {
        try
        {
            return Json(_csv2JsonTransform.Transform(csv));
        }
        catch (FormatException e)
        {
            return Json(e.Message);
        }
    }

}