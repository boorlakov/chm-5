namespace chm_5.LinAlg;

public static class GeneralOperations
{
    /// <summary>
    /// Matrix multiplication in dense format
    /// </summary>
    /// <param name="m">matrix</param>
    /// <param name="b">vector to multiply</param>
    /// <returns>result vector</returns>
    public static double[] MatMul(double[,] m, double[] b)
    {
        var columns = m.GetLength(0);
        var rows = m.GetLength(1);

        var res = new double[rows];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                res[i] += m[i, j] * b[j];
            }
        }

        return res;
    }

    /// <summary>
    /// Calculates Euclidean norm of vector in R 
    /// </summary>
    /// <param name="x">Vector with double elements</param>
    /// <returns>Euclidean norm value of vector in R</returns>
    public static double Norm(double[] x) => Math.Sqrt(x.Sum(t => t * t));

    /// <summary>
    /// Finds minimal and maximum eigen values in given matrix
    /// </summary>
    /// <param name="matrix">where eigen values is founded</param>
    /// <param name="initApprox">initial approximation</param>
    /// <param name="maxIter">maximum number of iterations</param>
    /// <param name="eps">Wanted accuracy</param>
    /// <returns>minimal and maximum eigen values</returns>
    public static (double, double) FindEigenAbsValues(
        double[,] matrix,
        double[] initApprox,
        int maxIter,
        double eps
    )
    {
        return (FindMinEigenValue(matrix, initApprox, maxIter, eps), FindMaxEigenValue(matrix, initApprox, maxIter, eps));
    }

    /// <summary>
    /// Finds minimal eigen value in given matrix
    /// </summary>
    /// <param name="matrix">where eigen values is founded</param>
    /// <param name="initApprox">initial approximation</param>
    /// <param name="maxIter">maximum number of iterations</param>
    /// <param name="eps">Wanted accuracy</param>
    /// <returns>minimal eigen value</returns>
    public static double FindMinEigenValue(
        double[,] matrix,
        double[] initApprox,
        int maxIter,
        double eps
    )
    {
        var x = Gauss.Solve(matrix, initApprox);
        var lambdaPrev = 0.0;
        var lambda = Norm(x) / Norm(initApprox);

        for (var i = 1; i < maxIter && Math.Abs((lambda - lambdaPrev) / lambda) > eps; i++)
        {
            var xPrev = new double[x.Length];
            x.AsSpan().CopyTo(xPrev);

            x = Gauss.Solve(matrix, xPrev);

            var normX = Norm(x);

            if (i % 5 == 0)
            {
                for (var j = 0; j < x.Length; j++)
                {
                    x[j] /= normX;
                }
            }

            lambdaPrev = lambda;
            lambda = Norm(x) / Norm(xPrev);
        }

        return 1.0 / lambda;
    }

    /// <summary>
    /// Finds maximum eigen values in given matrix
    /// </summary>
    /// <param name="matrix">where eigen values is founded</param>
    /// <param name="initApprox">initial approximation</param>
    /// <param name="maxIter">maximum number of iterations</param>
    /// <param name="eps">Wanted accuracy</param>
    /// <returns>maximum eigen values</returns>
    public static double FindMaxEigenValue(
        double[,] matrix,
        double[] initApprox,
        int maxIter,
        double eps
    )
    {
        var x = MatMul(matrix, initApprox);
        var lambdaPrev = 0.0;
        var lambda = Norm(x) / Norm(initApprox);

        for (var i = 1; i < maxIter && Math.Abs((lambda - lambdaPrev) / lambda) > eps; i++)
        {
            var xPrev = new double[x.Length];
            x.AsSpan().CopyTo(xPrev);

            x = MatMul(matrix, xPrev);

            var normX = Norm(x);

            if (i % 5 == 0)
            {
                for (var j = 0; j < x.Length; j++)
                {
                    x[j] /= normX;
                }
            }

            lambdaPrev = lambda;
            lambda = normX / Norm(xPrev);
        }

        return lambda;
    }
}