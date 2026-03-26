using System;
using System.IO;

namespace MessageLoggerApp
{
    public class MessagePublisher
    {
        public event Action<string> MessageSent;

        public void Send(string message)
        {
            MessageSent?.Invoke(message);
        }
    }

    public class FileLogger
    {
        private readonly string _logFilePath;

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void LogMessage(string message)
        {
            string logEntry = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(_logFilePath, logEntry);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string logFile = "logPD24.txt";

            File.WriteAllText(logFile, string.Empty);

            MessagePublisher publisher = new MessagePublisher();
            FileLogger logger = new FileLogger(logFile);

            publisher.MessageSent += logger.LogMessage;

            Console.WriteLine("Будь ласка, введіть текст 4 рази:");

            for (int i = 1; i <= 4; i++)
            {
                Console.Write($"Рядок {i}: ");
                string input = Console.ReadLine();

                publisher.Send(input);
            }

            Console.WriteLine($"\nГотово! Ваші повідомлення збережено у файлі {logFile}.");
        }
    }
}