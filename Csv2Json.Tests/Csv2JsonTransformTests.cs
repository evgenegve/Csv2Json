using Xunit;
using Csv2Json.Services;
using Csv2Json.WebApi.Controllers;
using System;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Csv2Json.Tests;

public class Csv2JsonTransformTests
{
    Csv2JsonTransform csv2JsonTransform = new Csv2JsonTransform();

    [Fact]
    public void ValidateCsv_InputEpmty_ReturnFalse()
    {
        bool result = csv2JsonTransform.ValidateCsv("");

        Assert.False(result);
    }

    [Fact]
    public void ValidateCsv_InputBadCsv1_ReturnFalse()
    {
        string input = "id,name\n1,12,Andrey";
        bool result = csv2JsonTransform.ValidateCsv(input);

        Assert.False(result);
    }

    [Fact]
    public void ValidateCsv_InputBadCsv2_ReturnFalse()
    {
        string input = "id,Name,Age\n1,Evgeny,36\n2,Andrey";
        bool result = csv2JsonTransform.ValidateCsv(input);

        Assert.False(result);
    }
    [Fact]
    public void Csv2JsonTransform_InputEmptyString_ThrowFormatException()
    {
        Assert.Throws<FormatException>(() => csv2JsonTransform.Transform(""));
    }

    [Fact]
    public void Csv2JsonTransform_CorrectJson()
    {
        string csv = "id,Name,Age/n1,Evgeny,36/n2,Andrey,38/n3,Alexander,27";
        string expected = "[{\"id\":\"1\",\"Name\":\"Evgeny\",\"Age\":\"36\"},{\"id\":\"2\",\"Name\":\"Andrey\",\"Age\":\"38\"},{\"id\":\"3\",\"Name\":\"Alexander\",\"Age\":\"27\"}]";
        string result = csv2JsonTransform.Transform(csv);

        Assert.Equal(expected, result);
    }
    [Fact]
    public void Csv2JsonTransformController_ShouldBadRequest()
    {
        var mock = new Mock<ILogger<Csv2JsonTransformController>>();
        ILogger<Csv2JsonTransformController> logger = mock.Object;

        var csv2JsonTransformController = new Csv2JsonTransformController(
                                                                            logger,
                                                                            new Csv2JsonTransform()
                                                                            );

        var result = csv2JsonTransformController.Transform("");
        
        // Assert
        Assert.Equal(400, (result.Result as ObjectResult)?.StatusCode);
    }
}