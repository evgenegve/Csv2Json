using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Csv2Json.MVC.Models;
using Csv2Json.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

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

    [HttpPost]
    public JsonResult AjaxTransform(IFormFile fileCsv)
    {
        using (var reader = new StreamReader(fileCsv.OpenReadStream()))
        {
            var csv = reader.ReadToEnd();
            return Json(_csv2JsonTransform.Transform(csv));
        }
    }

    [HttpPost]
    public JsonResult SimpleTextMethod(string strValue1, string strValue2)
    {
        return Json($"i1:{strValue1}, s1:{strValue2}");
    }
}