using System;

class EquationSolver
{
    static void Main()
    {
        Console.WriteLine("Выберите метод решения:");
        Console.WriteLine("1 - Метод простых итераций");
        Console.WriteLine("2 - Метод Гаусса");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            SolveByIterationMethod();
        }
        else if (choice == 2)
        {
            SolveByGaussMethod();
        }
        else
        {
            Console.WriteLine("Неверный выбор!");
        }
    }

    static void SolveByIterationMethod()
    {
        double epsilon = 0.001;
        Console.WriteLine($"Метод простых итераций. Точность: {epsilon}");

        double[,] alpha = {
            { 0, -0.3, -0.1 },
            { -0.2, 0, -0.4 },
            { -0.1, -0.5, 0 }
        };

        double[] beta = { 1.2, 0.5, -1.0 };
        int n = beta.Length;

        Console.WriteLine("\nМатрица alpha:");
        PrintMatrix(alpha);
        Console.WriteLine("\nВектор beta:");
        PrintVector(beta);

        double[] xOld = new double[n];
        for (int i = 0; i < n; i++)
        {
            xOld[i] = beta[i];
        }

        double[] xNew = new double[n];
        double distance;
        int iteration = 0;

        do
        {
            iteration++;
            distance = 0;

            for (int i = 0; i < n; i++)
            {
                xNew[i] = beta[i];
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        xNew[i] += alpha[i, j] * xOld[j];
                }
            }

            for (int i = 0; i < n; i++)
            {
                distance += Math.Pow(xNew[i] - xOld[i], 2);
            }
            distance = Math.Sqrt(distance);

            Array.Copy(xNew, xOld, n);

            Console.WriteLine($"{iteration}: x1 = {xNew[0]:F4}, x2 = {xNew[1]:F4}, x3 = {xNew[2]:F4}, d = {distance:F6}");

        } while (distance > epsilon);

        Console.WriteLine("\nРешение:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"x[{i + 1}] = {xNew[i]:F4}");
        }
    }

    static void SolveByGaussMethod()
    {
        Console.WriteLine("Метод Гаусса");

        double[,] alpha = {
            { 0, -0.3, -0.1 },
            { -0.2, 0, -0.4 },
            { -0.1, -0.5, 0 }
        };

        double[] beta = { 1.2, 0.5, -1.0 };
        int n = beta.Length;

        Console.WriteLine("\nМатрица alpha:");
        PrintMatrix(alpha);
        Console.WriteLine("\nВектор beta:");
        PrintVector(beta);

        // Преобразуем матрицу alpha и beta в расширенную матрицу
        double[,] matrix = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = (i == j ? 1 : alpha[i, j]); // Диагональные элементы заменяем на 1
            }
            matrix[i, n] = beta[i]; // Добавляем столбец beta
        }

        Console.WriteLine("\nРасширенная матрица перед преобразованиями:");
        PrintMatrix(matrix);

        // Прямой ход метода Гаусса
        for (int i = 0; i < n; i++)
        {
            Pivot(matrix, i);

            double diagElement = matrix[i, i];
            for (int j = 0; j <= n; j++)
            {
                matrix[i, j] /= diagElement;
            }

            for (int k = i + 1; k < n; k++)
            {
                double factor = matrix[k, i];
                for (int j = 0; j <= n; j++)
                {
                    matrix[k, j] -= factor * matrix[i, j];
                }
            }
        }

        // Обратный ход
        double[] solution = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            solution[i] = matrix[i, n];
            for (int j = i + 1; j < n; j++)
            {
                solution[i] -= matrix[i, j] * solution[j];
            }
        }

        Console.WriteLine("\nРешение:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"x[{i + 1}] = {solution[i]:F4}");
        }
    }

    static void Pivot(double[,] matrix, int row)
    {
        int n = matrix.GetLength(0);
        int maxRow = row;

        for (int i = row + 1; i < n; i++)
        {
            if (Math.Abs(matrix[i, row]) > Math.Abs(matrix[maxRow, row]))
            {
                maxRow = i;
            }
        }

        for (int j = 0; j <= n; j++)
        {
            double temp = matrix[row, j];
            matrix[row, j] = matrix[maxRow, j];
            matrix[maxRow, j] = temp;
        }
    }

    static void PrintMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j]:F4}\t");
            }
            Console.WriteLine();
        }
    }

    static void PrintVector(double[] vector)
    {
        foreach (var value in vector)
        {
            Console.WriteLine($"{value:F4}");
        }
    }
}
