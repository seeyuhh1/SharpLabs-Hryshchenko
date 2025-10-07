using System;

namespace Task1
{
    public class Program
    {
        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public static string GetMessage(int number)
        {
            return IsEven(number) ? "Двері відкриваються!" : "Двері зачинені...";
        }

        public static void Main(string[] args)
        {
            Console.Write("Введіть число: ");
            int num = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(GetMessage(num));
        }
    }
}
