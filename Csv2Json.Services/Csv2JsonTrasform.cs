using System.Text;

namespace Csv2Json.Services;
public class Csv2JsonTransform : ICsv2JsonTransform
{
    public bool ValidateCsv(string csv)
    {
        if (String.IsNullOrEmpty(csv))
            return false;
        
        var strings = csv.Split("/n");
        int num = strings[0].Split(',').Length;
        
        for (int i = 1; i < strings.Length; i++)
            if (strings[i].Split(',').Length != num)
                return false;
            
        return true;
    }

    public string Transform(string csv)
    {
        if (!ValidateCsv(csv))
            return "Некорректный формат";
        
        var strings = csv.Split("/n");
        string[] fields = strings[0].Split(',');
        string[] data = new string[strings.Length - 1];

        for (int i = 1; i < strings.Length; i++)
            data[i-1] = "{" + String.Join(",", strings[i].Split(',').Select((x,j) => $"\"{fields[j]}\":\"{x}\"")) + "}";
        
        
        return $"[{String.Join(",", data)}]";
    }
}
