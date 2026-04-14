using System;
using System.Collections.Generic;

namespace PracticalWork1
{
    // Оголошення делегатів 1, 2, 3, 6
    // Завдання 1
    delegate double MathOperation(double a, double b);

    // Завдання 2
    delegate void NotificationHandler(string message);

    // Завдання 3
    delegate bool FilterPredicate(int number);

    // Завдання 6
    delegate bool Validator(string text);

    // Клас для завдання 5
    class Logger
    {
        public Action<string> LogHandler;

        public void Log(string message)
        {
            if (LogHandler != null)
            {
                LogHandler(message);
            }
        }
    }

    // Головний клас
    class Program
    {
        static double Add(double a, double b) { return a + b; }
        static double Subtract(double a, double b) { return a - b; }
        static double Multiply(double a, double b) { return a * b; }
        static double Divide(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("Помилка: Ділення на нуль!");
                return 0;
            }
            return a / b;
        }

        static void SendEmail(string message)
        {
            Console.WriteLine($"Email sent: [{message}]");
        }

        static void SendSMS(string message)
        {
            Console.WriteLine($"SMS sent: [{message}]");
        }

        static void FilterArray(int[] numbers, FilterPredicate predicate)
        {
            foreach (int num in numbers)
            {
                if (predicate(num))
                {
                    Console.Write(num + " ");
                }
            }
            Console.WriteLine();
        }

        static bool IsEven(int n) { return n % 2 == 0; }
        static bool IsGreaterThan5(int n) { return n > 5; }

        static Validator GetValidator(int minLength)
        {
            return text => text != null && text.Length >= minLength;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАВДАННЯ 1: Калькулятор ===");
            MathOperation operation;

            operation = Add;
            Console.WriteLine($"Додавання (10 + 5): {operation(10, 5)}");

            operation = Subtract;
            Console.WriteLine($"Віднімання (10 - 5): {operation(10, 5)}");

            operation = Multiply;
            Console.WriteLine($"Множення (10 * 5): {operation(10, 5)}");

            operation = Divide;
            Console.WriteLine($"Ділення (10 / 5): {operation(10, 5)}");


            Console.WriteLine("\n=== ЗАВДАННЯ 2: Мультикастинг ===");
            NotificationHandler notify = SendEmail;
            notify += SendSMS;

            notify("Привіт! Це тестове повідомлення.");


            Console.WriteLine("\n=== ЗАВДАННЯ 3: Фільтрація списку ===");
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.Write("Парні числа: ");
            FilterArray(numbers, IsEven);

            Console.Write("Числа більше 5: ");
            FilterArray(numbers, IsGreaterThan5);

            Console.Write("Непарні числа (через лямбду): ");
            FilterArray(numbers, n => n % 2 != 0);


            Console.WriteLine("\n=== ЗАВДАННЯ 4: Вбудовані делегати (Func та Action) ===");
            Func<double, double, double> funcCalc = Add;
            Console.WriteLine($"Func Додавання (20 + 30): {funcCalc(20, 30)}");

            List<string> students = new List<string> { "Олександр", "Анна", "Андрій", "Іван", "Марія" };

            List<string> aStudents = students.FindAll(name => name.StartsWith("А"));

            Console.Write("Студенти на літеру 'А': ");
            foreach (var student in aStudents)
            {
                Console.Write(student + " ");
            }
            Console.WriteLine();


            Console.WriteLine("\n=== ЗАВДАННЯ 5: Логування ===");
            Logger logger = new Logger();

            logger.LogHandler = message => Console.WriteLine($"Standard Log: {message}");
            logger.Log("Програма працює нормально.");

            logger.LogHandler = message => Console.WriteLine($"UPPERCASE LOG: {message.ToUpper()}");
            logger.Log("Увага, сталася помилка!");


            Console.WriteLine("\n=== ЗАВДАННЯ 6: Динамічний валідатор тексту ===");

            Validator passwordValidator = GetValidator(8);
            Validator loginValidator = GetValidator(3);

            string testPassword = "qwerty";
            string testLogin = "admin";

            Console.WriteLine($"Чи підходить пароль '{testPassword}': {passwordValidator(testPassword)}");
            Console.WriteLine($"Чи підходить пароль 'supersecret123': {passwordValidator("supersecret123")}");

            Console.WriteLine($"Чи підходить логін 'ab': {loginValidator("ab")}");
            Console.WriteLine($"Чи підходить логін '{testLogin}': {loginValidator(testLogin)}");

            Console.ReadLine();
        }
    }
}