using System;

namespace PracticalWork1
{
    class Program
    {
        static bool IsSymmetric(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            if (n != m)
                throw new ArgumentException("Матрица должна быть квадратной!");//дл

            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (matrix[i, j] != matrix[j, i])
                        return false;

            return true;
        }

        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int value) && value > 0)
                    return value;
                Console.WriteLine("  ! Введите целое положительное число.");
            }
        }

        static double[] ReadRow(int index, int n)
        {
            while (true)
            {
                Console.Write($"  Строка {index + 1}: ");
                string? input = Console.ReadLine();
                string[] parts = (input ?? "").Split(
                    new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != n)
                {
                    Console.WriteLine($"  ! Нужно ввести ровно {n} чисел (введено {parts.Length}).");
                    continue;
                }

                double[] row = new double[n];
                bool ok = true;
                for (int i = 0; i < n; i++)
                {
                    string p = parts[i].Replace(',', '.');
                    if (!double.TryParse(p, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out row[i]))
                    {
                        Console.WriteLine($"  ! Элемент «{parts[i]}» не является числом.");
                        ok = false;
                        break;
                    }
                }
                if (ok) return row;
            }
        }

        static double[,] ReadMatrix()
        {
            Console.WriteLine("Ввод матрицы");
            int n = ReadInt("Введите размерность квадратной матрицы n: ");
            Console.WriteLine($"Введите {n} строк по {n} вещественных чисел (через пробел):");

            double[,] matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                double[] row = ReadRow(i, n);
                for (int j = 0; j < n; j++)
                    matrix[i, j] = row[j];
            }
            return matrix;
        }

        static void PrintMatrix(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            Console.WriteLine("\nВведённая матрица:");
            for (int i = 0; i < n; i++)
            {
                Console.Write("  ");
                for (int j = 0; j < n; j++)
                    Console.Write($"{matrix[i, j],8:g}");
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(" Проверка симметричности матрицы относительно");
            Console.WriteLine(" главной диагонали (вариант 3)");

            double[,] matrix = ReadMatrix();
            PrintMatrix(matrix);

            if (IsSymmetric(matrix))
                Console.WriteLine("\n>>> РЕЗУЛЬТАТ: матрица СИММЕТРИЧНА относительно главной диагонали.");
            else
                Console.WriteLine("\n>>> РЕЗУЛЬТАТ: матрица НЕ СИММЕТРИЧНА относительно главной диагонали.");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}