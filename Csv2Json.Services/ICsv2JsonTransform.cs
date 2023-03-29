public interface ICsv2JsonTransform
{
    public bool ValidateCsv(string csv);
    public string Transform(string csv);
}