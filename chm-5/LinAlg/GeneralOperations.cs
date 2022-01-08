namespace chm_5.LinAlg;

public static class GeneralOperations
{
    /// <summary>
    /// Calculates Euclidean norm of vector in R 
    /// </summary>
    /// <param name="x">Vector with double elements</param>
    /// <returns>Euclidean norm value of vector in R</returns>
    public static double Norm(double[] x) => Math.Sqrt(x.Sum(t => t * t));

    /// <summary>
    /// Finds minimal and maximum eigen values in given matrix
    /// </summary>
    /// <param name="matrix">Where eigen values is founded</param>
    /// <param name="initApprox">initial approximation</param>
    /// <returns>minimal and maximum eigen values</returns>
    public static (double minEigenValue, double maxEigenValue) FindEigenValues(double[,] matrix, double[] initApprox)
    {
        return (FindMinEigenValue(matrix, initApprox), FindMaxEigenValue(matrix, initApprox));
    }

    /// <summary>
    /// Finds minimal and maximum eigen values in given matrix
    /// </summary>
    /// <param name="matrix">Where eigen values is founded</param>
    /// <param name="initApprox">initial approximation</param>
    /// <returns>minimal and maximum eigen values</returns>
    public static double FindMinEigenValue(double[,] matrix, double[] initApprox)
    {

    }

    /// <summary>
    /// Finds minimal and maximum eigen values in given matrix
    /// </summary>
    /// <param name="matrix">Where eigen values is founded</param>
    /// <param name="initApprox">initial approximation</param>
    /// <returns>minimal and maximum eigen values</returns>
    public static double FindMaxEigenValue(double[,] matrix, double[] initApprox)
    {

    }
}