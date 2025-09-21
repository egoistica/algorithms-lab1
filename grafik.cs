using System.Diagnostics;
using ScottPlot;

internal class Program
{
    public static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        Random rand = new Random();
        
        List<int> sizes = new List<int>();
        List<double> times = new List<double>();
        
        for (int n = 1; n <= 65000; n++)
        {
            double[] array = new double[n];
            for (int i = 1; i < n; i++)
            {
                array[i] = rand.NextDouble();
            }
            sw.Restart();
            QuickSort(array, 0, array.Length - 1);
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            
            double elapsedSeconds = sw.Elapsed.TotalSeconds;
            sizes.Add(n);
            times.Add(elapsedSeconds);

            Console.WriteLine($"Для {n} элементов время выполнения алгоритма составило: {ts}");
        }
        
        PlotGraph(sizes, times);
        
    }

    public static double[] QuickSort(double[] arr, int start, int end)
    {
        if (end <= start)
        {
            return arr;
        }

        else
        {
            double pivot = arr[start];
            int pivotIndex = start;

            
            for (int i = start + 1; i <= end; i++)
            {
                if (arr[i] < pivot)
                {
                    pivotIndex++;
                    double temp = arr[i];
                    arr[i] = arr[pivotIndex];
                    arr[pivotIndex] = temp;
                }
            }
            
            // Помещаем опорный элемент на правильную позицию
            double temp2 = arr[start];
            arr[start] = arr[pivotIndex];
            arr[pivotIndex] = temp2;
            
            QuickSort(arr, start, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, end);
            return arr;
        }
    }
    
    public static void PlotGraph(List<int> sizes, List<double> times)
    {
        // Создаем график
        Plot plt = new Plot();
    
        // Преобразуем данные в массивы double
        double[] xValues = new double[sizes.Count];
        double[] yValues = new double[times.Count];
    
        for (int i = 0; i < sizes.Count; i++)
        {
            xValues[i] = sizes[i];
            yValues[i] = times[i];
        }
    
        // Добавляем данные на график
        plt.Add.Scatter(xValues, yValues);
        plt.XLabel("Количество элементов");
        plt.YLabel("Время выполнения (секунды)");
        plt.Title("Зависимость времени выполнения быстрой сортировки");
    
        // Сохраняем график в файл
        plt.SavePng("graph.png", 600, 400);
    
        Console.WriteLine("График сохранен в файл graph.png");
    }
}