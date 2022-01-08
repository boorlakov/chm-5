namespace chm_5.LinAlg;

public static class Gauss
{
    /// <summary>
    /// Solves SLAE like Ax = b with Gauss method
    /// </summary>
    /// <param name="matrixA">Matrix in dense format</param>
    /// <param name="vectorB">Vector of right side</param>
    /// <returns>Solution vector</returns>
    public static double[] Solve(double[,] matrixA, double[] vectorB)
    {
        var extendedMatrix = Elimination(matrixA, vectorB);

        var vectorX = BackSubstitution(extendedMatrix);

        return vectorX;
    }

    private static double[] BackSubstitution(double[,] extendedMatrix)
    {
        var rowSize = extendedMatrix.GetLength(Axis.X);
        var colSize = extendedMatrix.GetLength(Axis.Y);

        var vectorX = new double[rowSize];

        for (var row = rowSize - 1; row >= 0; row--)
        {
            vectorX[row] = extendedMatrix[row, colSize - 1];

            for (var col = row + 1; col < rowSize; col++)
            {
                vectorX[row] -= extendedMatrix[row, col] * vectorX[col];
            }

            vectorX[row] /= extendedMatrix[row, row];
        }

        return vectorX;
    }

    private static double[,] Elimination(double[,] matrixA, double[] vectorB)
    {
        var rowSize = matrixA.GetLength(Axis.X);
        var colSize = rowSize + 1;

        var extendedMatrix = ExtendMatrix(matrixA, vectorB);

        var pivotRow = 0;
        var pivotCol = 0;

        while (pivotRow < rowSize && pivotCol < colSize)
        {
            var maxRow = ArgMax(pivotRow, rowSize, pivotCol, extendedMatrix);

            if (extendedMatrix[maxRow, pivotCol] == 0.0)
            {
                pivotCol++;
            }
            else
            {
                SwapRows(pivotRow, maxRow, extendedMatrix);

                for (var row = pivotRow + 1; row < rowSize; row++)
                {
                    var f = extendedMatrix[row, pivotCol] / extendedMatrix[pivotRow, pivotCol];
                    extendedMatrix[row, pivotCol] = 0.0;

                    for (var col = pivotCol + 1; col < colSize; col++)
                    {
                        extendedMatrix[row, col] -= extendedMatrix[pivotRow, col] * f;
                    }
                }

                pivotRow++;
                pivotCol++;
            }
        }

        return extendedMatrix;
    }

    private static void SwapRows(int srcRow, int dstRow, double[,] matrixA)
    {
        var colSize = matrixA.GetLength(Axis.Y);

        for (var col = 0; col < colSize; col++)
        {
            Swap(ref matrixA[srcRow, col], ref matrixA[dstRow, col]);
        }
    }

    private static double[,] ExtendMatrix(double[,] matrixA, double[] vectorB)
    {
        var rowSize = matrixA.GetLength(Axis.X);
        var colSize = rowSize + 1;

        var extendedMatrix = new double[rowSize, colSize];

        for (var row = 0; row < rowSize; row++)
        {
            for (var col = 0; col < rowSize; col++)
            {
                extendedMatrix[row, col] = matrixA[row, col];
            }
        }

        for (var row = 0; row < rowSize; row++)
        {
            extendedMatrix[row, colSize - 1] = vectorB[row];
        }

        return extendedMatrix;
    }

    private static void Swap(ref double src, ref double dst) => (src, dst) = (dst, src);

    private static int ArgMax(int srcRow, int dstRow, int pivotCol, double[,] matrixA)
    {
        // argmax equals to 0, because row (in theory) can have only zero-elements.
        var maxRow = 0;
        var elem = 0.0;

        for (var row = srcRow; row < dstRow; row++)
        {
            if (elem < Math.Abs(matrixA[row, pivotCol]))
            {
                elem = Math.Abs(matrixA[row, pivotCol]);
                maxRow = row;
            }
        }

        return maxRow;
    }

    private abstract class Axis
    {
        public const int X = 0;
        public const int Y = 1;
    }
}