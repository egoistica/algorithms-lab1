using System;
using System.Diagnostics;
using ScottPlot;

public class Program
{
    public static void Main()
    {
        
    }
}

public class Matrix
{
    private int[,] data;
    public int Rows { get; }
    public int Cols { get; }

    public Matrix()
    {
        Rows = 0;
        Cols = 0;
        data = new int[0, 0];
    }

    public Matrix(int n, int m)
    {
        if (n <= 0 || m <= 0)
            throw new ArgumentException("Размеры матрицы должны быть положительными");

        Rows = n;
        Cols = m;
        data = new int[n, m];
    }

    public int this[int i, int j]
    {
        get => data[i, j];
        set => data[i, j] = value;
    }

    public void FillRandom()
    {
        var rnd = new Random();

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                data[i, j] = rnd.Next();
        }
    }

    public void Print()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                Console.Write(data[i, j] + " ");
            Console.WriteLine();
        }
    }
}

public class AlgorithmsBencmark
{ 
    // Алгоритмы для векторов (1-3)
    
    // 1. f(v) = 1 (постоянная функция)
    public static double ConstFunction(double[] vector)
    {
        // Тяжёлая константная функция через синусы - O(1)
        double result = 0.0;
        for (int i = 0; i < 1000; i++)
        {
            result += Math.Sin(i * 0.001) + Math.Cos(i * 0.001);
        }
        return 1.0; // Возвращаем константу
    }
    
    // 2. f(v) = sum от k=1 до n v_k (сумма элементов)
    public static double SumFunction(double[] vector)
    {
        double sum = 0.0;
        for (int i = 0; i < vector.Length; i++)
        {
            sum += vector[i];
        }
        return sum;
    }
    
    // 3. f(v) = произведение от k=1 до n v_k (произведение элементов)
    public static double ProductFunction(double[] vector)
    {
        double product = 1.0;
        for (int i = 0; i < vector.Length; i++)
        {
            product *= vector[i];
        }
        return product;
    }
    
    // III. Пирамидальная сортировка (HeapSort) - O(n log n)
    public static void HeapSort(int[] arr)
    {
        int n = arr.Length;

        // Построение кучи (перегруппируем массив)
        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i);

        // Один за другим извлекаем элементы из кучи
        for (int i = n - 1; i >= 0; i--)
        {
            // Перемещаем текущий корень в конец
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            // вызываем процедуру heapify на уменьшенной куче
            Heapify(arr, i, 0);
        }
    }

    // Процедура для преобразования в двоичную кучу поддерева с корневым узлом i
    private static void Heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int l = 2 * i + 1; // left = 2*i + 1
        int r = 2 * i + 2; // right = 2*i + 2

        // Если левый дочерний элемент больше корня
        if (l < n && arr[l] > arr[largest])
            largest = l;

        // Если правый дочерний элемент больше, чем самый большой элемент на данный момент
        if (r < n && arr[r] > arr[largest])
            largest = r;

        // Если самый большой элемент не корень
        if (largest != i)
        {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;

            // Рекурсивно преобразуем в двоичную кучу затронутое поддерево
            Heapify(arr, n, largest);
        }
    }

    // TODO: Доделать алгоритмы 4-7
    // Для вектора сделать только алгоритмы, инициализировать и заполнять значениями нужно в Main

    // Алгоритм умножения матриц

    public static int[,] MultiplyMatrix(int[,] matrixA, int[,] matrixB)
    {
        var n = matrixA.GetLength(0);
        var m = matrixA.GetLength(1);
        var b = matrixB.GetLength(1);

        var result = new int[n, b];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < b; j++)
            {
                var sum = 0;

                for (int k = 0; k < m; k++)
                {
                    sum += matrixA[i, k] * matrixB[k, j];
                }

                result[i, j] = sum;
            }
        }

        return result;
    }

    // Алгоритмы возведения в степень

    public static long Pow(long x, int n)
    {
        if (n < 0)
            throw new ArgumentException("Степень должна быть неотрицательной!", nameof(n));

        long result = 1;

        for (int i = 0; i < n; i++)
            result *= x;

        return result;
    }

    public static long RecPow(long x, int n)
    {
        if (n < 0)
            throw new ArgumentException("Степень должна быть неотрицательной!", nameof(n));

        if (n == 0)
            return 1;

        long result = RecPow(x, n / 2);

        if (n % 2 != 0)
            return result * result * x;
        else
            return result * result;
    }

    public static long QuickPow(long x, int n)
    {
        if (n < 0)
            throw new ArgumentException("Степень должна быть неотрицательной!", nameof(n));

        long result = 1;
        long baseVal = x;
        int exponent = n;

        while (exponent > 0)
        {
            if (exponent % 2 == 1)
                result *= baseVal;

            baseVal *= baseVal;
            exponent /= 2;
        }

        return result;
    }

    public static long ClassicalQuickPow(long x, int n)
    {
        if (n < 0)
            throw new ArgumentException("Степень должна быть неотрицательной!", nameof(n));

        long result = 1;
        long baseVal = x;
        int exponent = n;

        while (exponent > 0)
        {
            if (exponent % 2 == 0)
            {
                baseVal *= baseVal;
                exponent /= 2;
            }
            else
            {
                result *= baseVal;
                exponent--;
            }
        }

        return result;
    }

    // TODO: Доделать алгоритмы сложности > O(n)
}
