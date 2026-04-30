using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Логіка для Завдання 5
        if (args.Length > 0)
        {
            FileAnalyzerCLI.Run(args[0]);
            return;
        }

        while (true)
        {
            Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ ===");
            Console.WriteLine("1. Аналізатор текстового файлу (story.txt)");
            Console.WriteLine("2. Інспектор папки");
            Console.WriteLine("3. Пошук найбільшого файлу");
            Console.WriteLine("4. Очищення кешу");
            Console.WriteLine("0. Вихід");
            Console.Write("Оберіть дію: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введіть шлях до story.txt (або створіть його поруч з .exe): ");
                    string inPath = Console.ReadLine();
                    TextAnalyzer.AnalyzeText(inPath, "report.txt");
                    break;
                case "2":
                    Console.Write("Введіть шлях до папки: ");
                    FolderInspector.Inspect(Console.ReadLine());
                    break;
                case "3":
                    Console.Write("Введіть шлях до папки: ");
                    LargestFileFinder.Find(Console.ReadLine());
                    break;
                case "4":
                    Console.Write("Введіть шлях до папки cache: ");
                    string cachePath = Console.ReadLine();
                    Console.WriteLine("Оберіть метод (1 - Ітеративний, 2 - Рекурсивний): ");
                    if (Console.ReadLine() == "1")
                        CacheCleaner.ClearIterative(cachePath);
                    else
                    {
                        int delCount = 0;
                        long totSize = 0;
                        CacheCleaner.ClearRecursive(cachePath, ref delCount, ref totSize);
                        Console.WriteLine($"Видалено: {delCount}, Звільнено: {totSize} байт");
                    }
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }
}

// Завдання 1
class TextAnalyzer
{
    public static void AnalyzeText(string inputPath, string outputPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Файл не знайдено!");
            return;
        }

        int linesCount = 0, wordsCount = 0, charsCount = 0;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                linesCount++;
                charsCount += line.Length;
                wordsCount += line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
            }
        }

        File.WriteAllText(outputPath, $"Lines: {linesCount}\nWords: {wordsCount}\nCharacters: {charsCount}");
        Console.WriteLine("Звіт збережено у " + outputPath);
    }
}

// Завдання 2
class FolderInspector
{
    public static void Inspect(string path)
    {
        if (!Directory.Exists(path)) { Console.WriteLine("Шлях не існує."); return; }

        DirectoryInfo dir = new DirectoryInfo(path);
        Console.WriteLine("\n** ПІДПАПКИ **");
        foreach (var subDir in dir.GetDirectories()) Console.WriteLine($"- {subDir.Name}");

        Console.WriteLine("\n** ФАЙЛИ **");
        foreach (var file in dir.GetFiles())
            Console.WriteLine($"- {file.Name} ({file.Length} байт) | Створено: {file.CreationTime}");
    }
}

// Завдання 3
class LargestFileFinder
{
    public static void Find(string path)
    {
        if (!Directory.Exists(path)) { Console.WriteLine("Шлях не існує."); return; }

        var largestFile = new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories)
                                                 .OrderByDescending(f => f.Length)
                                                 .FirstOrDefault();

        if (largestFile != null)
        {
            Console.WriteLine($"Name: {largestFile.Name}\nSize: {largestFile.Length} bytes\nPath: {largestFile.FullName}");
        }
        else Console.WriteLine("Файлів не знайдено.");
    }
}

// Завдання 4
class CacheCleaner
{
    public static void ClearIterative(string path)
    {
        if (!Directory.Exists(path)) return;

        long totalSize = 0;
        int deletedCount = 0;
        Stack<string> directories = new Stack<string>();
        directories.Push(path);

        while (directories.Count > 0)
        {
            string currentDir = directories.Pop();
            foreach (string file in Directory.GetFiles(currentDir))
            {
                FileInfo fi = new FileInfo(file);
                totalSize += fi.Length;
                fi.Delete();
                deletedCount++;
            }
            foreach (string subDir in Directory.GetDirectories(currentDir)) directories.Push(subDir);
        }
        Console.WriteLine($"Видалено файлів: {deletedCount}\nЗвільнено місця: {totalSize} байт");
    }

    public static void ClearRecursive(string path, ref int deletedCount, ref long totalSize)
    {
        if (!Directory.Exists(path)) return;

        foreach (string file in Directory.GetFiles(path))
        {
            FileInfo fi = new FileInfo(file);
            totalSize += fi.Length;
            fi.Delete();
            deletedCount++;
        }
        foreach (string subDir in Directory.GetDirectories(path)) ClearRecursive(subDir, ref deletedCount, ref totalSize);
    }
}

// Завдання 5
class FileAnalyzerCLI
{
    public static void Run(string targetPath)
    {
        if (!Directory.Exists(targetPath))
        {
            Console.WriteLine($"Помилка: Папку '{targetPath}' не знайдено.");
            return;
        }

        DirectoryInfo dir = new DirectoryInfo(targetPath);
        var allFiles = dir.GetFiles("*", SearchOption.AllDirectories);

        Console.WriteLine($"Folders: {dir.GetDirectories("*", SearchOption.AllDirectories).Length}");
        Console.WriteLine($"Files: {allFiles.Length}");
        Console.WriteLine($"Total size: {Math.Round(allFiles.Sum(f => f.Length) / (1024.0 * 1024.0), 2)} MB");

        var largestFile = allFiles.OrderByDescending(f => f.Length).FirstOrDefault();
        Console.WriteLine($"Largest file: {(largestFile != null ? largestFile.Name : "Немає файлів")}");
    }
}