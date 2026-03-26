using System;
using System.IO;

namespace TextProcessorApp
{
    public delegate string TextOperation(string text);

    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "textPD24.txt";
            string outputFile = "resultPD24.txt";

            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Помилка: Файл {inputFile} не знайдено у робочій папці програми.");
                return;
            }

            File.WriteAllText(outputFile, string.Empty);

            ProcessFile(inputFile, outputFile, ToUpperCase);
            ProcessFile(inputFile, outputFile, CountCharacters);
            ProcessFile(inputFile, outputFile, CountWords);

            Console.WriteLine("Завдання 1 виконано. Перевірте resultPD24.txt!");
        }

        static string ToUpperCase(string text)
        {
            return "UPPERCASE:\n" + text.ToUpper();
        }

        static string CountCharacters(string text)
        {
            return $"Кількість символів: {text.Length}";
        }

        static string CountWords(string text)
        {
            string[] words = text.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return $"Кількість слів: {words.Length}";
        }

        static void ProcessFile(string inputFile, string outputFile, TextOperation operation)
        {
            string content = File.ReadAllText(inputFile);
            string result = operation(content);
            File.AppendAllText(outputFile, result + Environment.NewLine + new string('-', 20) + Environment.NewLine);
        }
    }
}