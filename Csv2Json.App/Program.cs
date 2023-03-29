using Csv2Json.Services;

ICsv2JsonTransform csv2JsonTransform = new Csv2JsonTransform();
string csv = "id,Name,Age/n1,Evgeny,36/n2,Andrey,38/n3,Alexander,27";
string result = csv2JsonTransform.Transform(csv);

Console.WriteLine(result);
