int matrixSize = 4;
int blockSize = 2;

int[,] result = new int[matrixSize, matrixSize];


int[,] firstMatrix = new int[,]
{
    {  1,  2,  3,  4 },
    {  5,  6,  7,  8 },
    {  9, 10, 11, 12 },
    { 13, 14, 15, 16 }
};

int[,] secondMatrix = new int[,]
{
    { 10, 10, 10, 10 },
    { 10, 10, 10, 10 },
    { 10, 10, 10, 10 },
    { 10, 10, 10, 10 }
};

ParallelMatrixMultiply(firstMatrix, secondMatrix, result, blockSize);

Console.WriteLine("Result Matrix:");
PrintMatrix(result);


static void ParallelMatrixMultiply(int[,] firstMatrix, int[,] secondMatrix, int[,] result, int blockSize)
{
    int rows = firstMatrix.GetLength(0);

    int numBlocks = rows / blockSize;

    Parallel.For(0, numBlocks, i =>
    {
        Parallel.For(0, numBlocks, j =>
        {
            for (int k = 0; k < numBlocks; k++)
            {
                Multiply(firstMatrix, secondMatrix, result, i, j, k, blockSize);
            }
        });
    });
}

static void Multiply(int[,] firstMatrix, int[,] secondMatrix, int[,] result, int i, int j, int k, int blockSize)
{
    int rows = firstMatrix.GetLength(0);

    for (int x = 0; x < blockSize; x++)
    {
        for (int y = 0; y < blockSize; y++)
        {
            for (int z = 0; z < blockSize; z++)
            {
                result[i * blockSize + x, j * blockSize + y] += firstMatrix[i * blockSize + x, k * blockSize + z] * secondMatrix[k * blockSize + z, j * blockSize + y];
            }
        }
    }
}

static void PrintMatrix(int[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Console.Write(matrix[i, j] + "\t");
        }
        Console.WriteLine();
    }
}