using System;

namespace Task5
{
    public class Program
    {
        public static double GetAverage(int[] marks)
        {
            int sum = 0;
            foreach (int mark in marks)
                sum += mark;

            return (double)sum / marks.Length;
        }

        public static int GetMin(int[] marks)
        {
            int min = marks[0];
            foreach (int mark in marks)
                if (mark < min) min = mark;

            return min;
        }

        public static int GetMax(int[] marks)
        {
            int max = marks[0];
            foreach (int mark in marks)
                if (mark > max) max = mark;

            return max;
        }

        public static void PrintGroupStatistics(int[][] groups)
        {
            for (int i = 0; i < groups.Length; i++)
            {
                int[] group = groups[i];
                Console.WriteLine(
                    $"Група {i + 1}: Середній = {GetAverage(group):F2}, " +
                    $"Мінімальний = {GetMin(group)}, " +
                    $"Максимальний = {GetMax(group)}"
                );
            }
        }

        public static void Main(string[] args)
        {
            int[][] groups = new int[][]
            {
                new int[] { 90, 75, 82, 100, 60, 88, 95, 70, 85, 92 },
                new int[] { 55, 60, 78, 80, 95, 66, 74, 89, 71, 50, 90, 83 },
                new int[] { 100, 95, 98, 92, 96, 90, 94, 100, 99 }
            };

            PrintGroupStatistics(groups);
        }
    }
}
