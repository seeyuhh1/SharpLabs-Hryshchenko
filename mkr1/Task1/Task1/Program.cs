using System;
using System.IO;

namespace DelegateFileProcessing
{
    public delegate string TextOperation(string text);

    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "textPD2X.txt";
            string outputFile = "resultPD2X.txt";

            if (!File.Exists(inputFile))
            {
                File.WriteAllText(inputFile, "Привіт світ!\nЦе тестовий файл з кількома рядками.\nСьогодні ми вивчаємо делегати в C#.");
            }

            File.WriteAllText(outputFile, string.Empty);

            Console.WriteLine("Починаємо обробку файлу...\n");

            ProcessFile(inputFile, outputFile, ToUpperCase);
            ProcessFile(inputFile, outputFile, CountCharacters);
            ProcessFile(inputFile, outputFile, CountWords);

            Console.WriteLine($"Обробку завершено! Перевірте файл {outputFile} у папці з програмою.");
        }

        static void ProcessFile(string inputFilePath, string outputFilePath, TextOperation operation)
        {
            try
            {
                string text = File.ReadAllText(inputFilePath);

                string result = operation(text);

                string formattedOutput = $"--- Результат операції {operation.Method.Name} ---\n{result}\n\n";

                File.AppendAllText(outputFilePath, formattedOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка при обробці файлу: {ex.Message}");
            }
        }

        static string ToUpperCase(string text)
        {
            return text.ToUpper();
        }

        static string CountCharacters(string text)
        {
            return $"Кількість символів: {text.Length}";
        }

        static string CountWords(string text)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n', '\t' };
            string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            
            return $"Кількість слів: {words.Length}";
        }
    }
}