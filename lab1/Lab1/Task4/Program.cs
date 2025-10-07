using System;

namespace Task4;

public class Program
{
    // Головний метод, який демонструє роботу функцій.
    // Усі сюжетні елементи видалено.
    public static void Main()
    {
        Console.Clear();
        Console.WriteLine("Програма для розрахунку параметрів трикутника.");

        // Запит сторін трикутника
        Console.Write("Введіть сторону a: ");
        if (!double.TryParse(Console.ReadLine(), out double a))
        {
            Console.WriteLine("Некоректний ввід.");
            return;
        }

        Console.Write("Введіть сторону b: ");
        if (!double.TryParse(Console.ReadLine(), out double b))
        {
            Console.WriteLine("Некоректний ввід.");
            return;
        }

        Console.Write("Введіть сторону c: ");
        if (!double.TryParse(Console.ReadLine(), out double c))
        {
            Console.WriteLine("Некоректний ввід.");
            return;
        }

        // Перевірка, чи можна утворити трикутник з введених сторін
        if (!IsValidTriangle(a, b, c))
        {
            Console.WriteLine("\nВведені сторони не можуть утворити трикутник.");
            return;
        }

        // Виведення результатів, якщо трикутник є дійсним
        Console.WriteLine("\nРезультати розрахунків:");
        Console.WriteLine($"Периметр: {GetPerimeter(a, b, c):F2}");
        Console.WriteLine($"Площа: {GetArea(a, b, c):F2}");
        Console.WriteLine($"Тип трикутника: {GetTriangleType(a, b, c)}");

        Console.ReadKey();
    }

    // Перевіряє, чи сторони можуть утворити трикутник.
    // Включає перевірку на те, чи сторони додатні.
    public static bool IsValidTriangle(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
        {
            return false;
        }
        return a + b > c && a + c > b && b + c > a;
    }

    // Обчислює периметр трикутника.
    public static double GetPerimeter(double a, double b, double c)
    {
        return a + b + c;
    }

    // Обчислює площу трикутника за формулою Герона.
    public static double GetArea(double a, double b, double c)
    {
        double p = GetPerimeter(a, b, c) / 2; // півпериметр
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    // Визначає тип трикутника.
    public static string GetTriangleType(double a, double b, double c)
    {
        // Використовуємо маленьке відхилення для порівняння чисел з плаваючою комою
        double epsilon = 0.0001;

        if (Math.Abs(a - b) < epsilon && Math.Abs(b - c) < epsilon)
        {
            return "рівносторонній";
        }

        if (Math.Abs(a - b) < epsilon || Math.Abs(a - c) < epsilon || Math.Abs(b - c) < epsilon)
        {
            return "рівнобедрений";
        }

        double a2 = a * a;
        double b2 = b * b;
        double c2 = c * c;

        if (Math.Abs(a2 + b2 - c2) < epsilon ||
            Math.Abs(a2 + c2 - b2) < epsilon ||
            Math.Abs(b2 + c2 - a2) < epsilon)
        {
            return "прямокутний";
        }

        return "довільний";
    }
}
