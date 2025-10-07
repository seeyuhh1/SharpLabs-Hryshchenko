using System;

namespace Task2
{
    public class Program
    {
        public static int[] GenerateRandomArray(int size, int min, int max)
        {
            Random rnd = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rnd.Next(min, max + 1);
            }
            return array;
        }

        public static int GetSum(int[] numbers)
        {
            int sum = 0;
            foreach (int n in numbers)
                sum += n;
            return sum;
        }

        public static double GetAverage(int[] numbers)
        {
            return (double)GetSum(numbers) / numbers.Length;
        }

        public static int GetMin(int[] numbers)
        {
            int min = numbers[0];
            foreach (int n in numbers)
                if (n < min) min = n;
            return min;
        }

        public static int GetMax(int[] numbers)
        {
            int max = numbers[0];
            foreach (int n in numbers)
                if (n > max) max = n;
            return max;
        }

        public static void Main(string[] args)
        {
            int[] array = GenerateRandomArray(10, 1, 100);

            Console.WriteLine("Масив:");
            Console.WriteLine(string.Join(", ", array));

            Console.WriteLine($"Сума: {GetSum(array)}");
            Console.WriteLine($"Середнє: {GetAverage(array):F2}");
            Console.WriteLine($"Мінімум: {GetMin(array)}");
            Console.WriteLine($"Максимум: {GetMax(array)}");
        }
    }
}
