using System.Text.Json;

namespace chm_5;

internal static class Program
{
    public static void Main(string[] args)
    {
        var content = File.ReadAllText("params.json");
        var inputParameters = JsonSerializer.Deserialize<JsonData>(content);
        var matrix = Utils.MatrixFromFile("matrix.txt", inputParameters!.InitApprox.Length);

        var (minEigenAbsValue, maxEigenAbsValue) = LinAlg.GeneralOperations.FindEigenAbsValues(
            matrix, 
            inputParameters!.InitApprox,
            inputParameters.MaxIter,
            inputParameters.Eps
            );

        Console.WriteLine($"Min = {minEigenAbsValue:G15}, Max = {maxEigenAbsValue:G15}");
    }
}

[Serializable]
internal class JsonData
{
    public double[] InitApprox { get; set; }

    public double Eps { get; set; }

    public int MaxIter { get; set; }
}