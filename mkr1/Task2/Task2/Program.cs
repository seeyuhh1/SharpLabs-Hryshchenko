using System;
using System.IO;

namespace EventLogging
{
    public class MessagePublisher
    {
        public delegate void MessageEventHandler(string message);

        public event MessageEventHandler MessageSent;

        public void Send(string message)
        {
            MessageSent?.Invoke(message);
        }
    }

    public class FileLogger
    {
        private readonly string _logFilePath = "logPD2X.txt";

        public FileLogger(MessagePublisher publisher)
        {
            publisher.MessageSent += LogMessage;
        }

        private void LogMessage(string message)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";

                File.AppendAllText(_logFilePath, logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запису у файл: {ex.Message}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string logFile = "logPD2X.txt";

            File.WriteAllText(logFile, string.Empty);

            MessagePublisher publisher = new MessagePublisher();

            FileLogger logger = new FileLogger(publisher);

            Console.WriteLine("Введіть 4 повідомлення:");

            for (int i = 1; i <= 4; i++)
            {
                Console.Write($"Рядок {i}: ");
                string input = Console.ReadLine();

                publisher.Send(input);
            }

            Console.WriteLine($"\nРоботу завершено! Перевірте лог-файл: {logFile}");
        }
    }
}