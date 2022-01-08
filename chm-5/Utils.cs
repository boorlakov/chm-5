using System.Text;

namespace chm_5;

public static class Utils
{
    public static double[,] MatrixFromFile(
        string matrixFileName,
        int size
    )
    {
        using var matrixFile = new StreamReader(matrixFileName);
        var matrix = new double[size, size];
        for (var i = 0; i < size; i++)
        {
            var line = ReadDoubles(matrixFile);
            for (var j = 0; j < size; j++)
            {
                matrix[i, j] = line[j];
            }  
        }

        return matrix;
    }

    private static double[] ReadDoubles(StreamReader file)
    {
        return file
            .ReadLine()!
            .Trim()
            .Split(' ')
            .Select(double.Parse)
            .ToArray();
    }
}