using Xunit;
using Csv2Json.Services;

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
    public void ValidateCsv_InputBadCsv_ReturnFalse()
    {
        string input = "id,name/n1,12,Andrey";
        bool result = csv2JsonTransform.ValidateCsv(input);
        
        Assert.False(result);
    }

    [Fact]
    public void Csv2JsonTransform_CorrectJson()
    {
        string csv = "id,Name,Age/n1,Evgeny,36/n2,Andrey,38/n3,Alexander,27";
        string expected = "[{\"id\":\"1\",\"Name\":\"Evgeny\",\"Age\":\"36\"},{\"id\":\"2\",\"Name\":\"Andrey\",\"Age\":\"38\"},{\"id\":\"3\",\"Name\":\"Alexander\",\"Age\":\"27\"}]";
        string result = csv2JsonTransform.Transform(csv);
        
        Assert.Equal(expected, result);
    }
}