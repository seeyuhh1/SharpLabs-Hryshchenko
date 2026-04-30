using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonSerializationTasks
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\nГоловне меню");
                Console.WriteLine("1. Завдання 1: Task Tracker (Збереження стану)");
                Console.WriteLine("2. Завдання 2: Серіалізація списку об'єктів (Студенти)");
                Console.WriteLine("3. Завдання 3: Циклічні посилання (Author <-> Book)");
                Console.WriteLine("4. Завдання 4: Серіалізація enum (OrderStatus)");
                Console.WriteLine("5. Завдання 5: Поліморфізм (Animal, Dog, Cat)");
                Console.WriteLine("6. Завдання 6: Вкладені об'єкти та Null (Player + Inventory)");
                Console.WriteLine("7. Завдання 7: Версійність моделей (Player Level)");
                Console.WriteLine("8. Завдання 8: Обробка помилок десеріалізації");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть завдання: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": RunTask1(); break;
                    case "2": RunTask2(); break;
                    case "3": RunTask3(); break;
                    case "4": RunTask4(); break;
                    case "5": RunTask5(); break;
                    case "6": RunTask6(); break;
                    case "7": RunTask7(); break;
                    case "8": RunTask8(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір. Спробуйте ще раз."); break;
                }
            }
        }

        #region Завдання 1: Task Tracker
        class TaskItem
        {
            public string Title { get; set; }
            public bool IsCompleted { get; set; }
        }

        static void RunTask1()
        {
            string filePath = "tasks.json";
            List<TaskItem> tasks = new List<TaskItem>();
            var options = new JsonSerializerOptions { WriteIndented = true };

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
                Console.WriteLine("[-] Стан задач відновлено з файлу.");
            }

            while (true)
            {
                Console.WriteLine("\n--- Task Tracker ---");
                Console.WriteLine("1. Додати задачу");
                Console.WriteLine("2. Змінити статус задачі");
                Console.WriteLine("3. Переглянути список");
                Console.WriteLine("0. Вийти в головне меню (зберегти)");
                Console.Write("Вибір: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Введіть назву задачі: ");
                    string title = Console.ReadLine();
                    tasks.Add(new TaskItem { Title = title, IsCompleted = false });
                }
                else if (choice == "2")
                {
                    for (int i = 0; i < tasks.Count; i++)
                        Console.WriteLine($"{i + 1}. {tasks[i].Title} - {(tasks[i].IsCompleted ? "[X]" : "[ ]")}");
                    Console.Write("Введіть номер задачі для зміни статусу: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
                    {
                        tasks[index - 1].IsCompleted = !tasks[index - 1].IsCompleted;
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("\nСписок задач:");
                    foreach (var t in tasks)
                        Console.WriteLine($"- {t.Title} [{(t.IsCompleted ? "Виконано" : "Не виконано")}]");
                }
                else if (choice == "0")
                {
                    string json = JsonSerializer.Serialize(tasks, options);
                    File.WriteAllText(filePath, json);
                    Console.WriteLine("[-] Задачі збережено. Вихід...");
                    break;
                }
            }
        }
        #endregion

        #region Завдання 2: Серіалізація списку об'єктів
        class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public double AverageScore { get; set; }
        }

        static void RunTask2()
        {
            var students = new List<Student>
            {
                new Student { Name = "Олексій", Age = 20, AverageScore = 95.5 },
                new Student { Name = "Марія", Age = 19, AverageScore = 88.0 },
                new Student { Name = "Іван", Age = 21, AverageScore = 76.5 },
                new Student { Name = "Анна", Age = 20, AverageScore = 91.2 },
                new Student { Name = "Петро", Age = 22, AverageScore = 82.3 }
            };

            string filePath = "students.json";
            var options = new JsonSerializerOptions { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(students, options);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine("Студентів серіалізовано у students.json");

            string readJson = File.ReadAllText(filePath);
            var deserializedStudents = JsonSerializer.Deserialize<List<Student>>(readJson);

            Console.WriteLine("\nДесеріалізовані студенти:");
            foreach (var s in deserializedStudents)
            {
                Console.WriteLine($"- {s.Name}, Вік: {s.Age}, Бал: {s.AverageScore}");
            }
        }
        #endregion

        #region Завдання 3: Циклічні посилання
        class Author
        {
            public string Name { get; set; }
            public List<Book> Books { get; set; } = new List<Book>();
        }

        class Book
        {
            public string Title { get; set; }
            public Author Author { get; set; }
        }

        static void RunTask3()
        {
            Author author = new Author { Name = "Тарас Шевченко" };
            Book book1 = new Book { Title = "Кобзар", Author = author };
            Book book2 = new Book { Title = "Катерина", Author = author };

            author.Books.Add(book1);
            author.Books.Add(book2);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            try
            {
                string json = JsonSerializer.Serialize(author, options);
                Console.WriteLine("Серіалізація пройшла успішно (збережено зв'язки):");
                Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
        #endregion

        #region Завдання 4: Серіалізація та десеріалізація enum
        enum OrderStatus
        {
            Pending,
            Processing,
            Completed
        }

        class Order
        {
            public int Id { get; set; }
            public OrderStatus Status { get; set; }
        }

        static void RunTask4()
        {
            var order = new Order { Id = 101, Status = OrderStatus.Processing };

            string jsonInt = JsonSerializer.Serialize(order);
            Console.WriteLine($"Без конвертера: {jsonInt}");

            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new JsonStringEnumConverter());

            string jsonString = JsonSerializer.Serialize(order, options);
            Console.WriteLine($"З JsonStringEnumConverter:\n{jsonString}");

            var deserializedOrder = JsonSerializer.Deserialize<Order>(jsonString, options);
            Console.WriteLine($"Десеріалізовано статус: {deserializedOrder.Status}");
        }
        #endregion

        #region Завдання 5: Polymorphism
        [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
        [JsonDerivedType(typeof(Dog), "dog")]
        [JsonDerivedType(typeof(Cat), "cat")]
        abstract class Animal
        {
            public string Name { get; set; }
        }

        class Dog : Animal
        {
            public int BarkVolume { get; set; }
        }

        class Cat : Animal
        {
            public int Lives { get; set; }
        }

        static void RunTask5()
        {
            List<Animal> animals = new List<Animal>
            {
                new Dog { Name = "Рекс", BarkVolume = 80 },
                new Cat { Name = "Мурзік", Lives = 9 }
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(animals, options);
            Console.WriteLine("Серіалізований список тварин:");
            Console.WriteLine(json);

            var deserializedAnimals = JsonSerializer.Deserialize<List<Animal>>(json, options);
            Console.WriteLine("\nПісля десеріалізації:");
            foreach (var animal in deserializedAnimals)
            {
                if (animal is Dog dog)
                    Console.WriteLine($"- Собака {dog.Name}, Гучність гавкоту: {dog.BarkVolume}");
                else if (animal is Cat cat)
                    Console.WriteLine($"- Кіт {cat.Name}, Життів: {cat.Lives}");
            }
        }
        #endregion

        #region Завдання 6 та 7: Вкладені об'єкти та Версійність моделей
        class Inventory
        {
            public List<string> Items { get; set; } = new List<string>();
        }

        class Player
        {
            public string Name { get; set; }

            public int Level { get; set; } = 1;

            public Inventory Inventory { get; set; }
        }

        static void RunTask6()
        {
            var player = new Player
            {
                Name = "Gamer123",
                Inventory = new Inventory { Items = { "Меч", "Зілля" } }
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(player, options);
            Console.WriteLine("Початковий JSON:");
            Console.WriteLine(json);

            string hackedJson = "{\n  \"Name\": \"Gamer123\"\n}";
            Console.WriteLine("\nJSON без Inventory та Level (стара версія):");
            Console.WriteLine(hackedJson);

            var deserializedPlayer = JsonSerializer.Deserialize<Player>(hackedJson, options);

            deserializedPlayer.Inventory ??= new Inventory();

            Console.WriteLine("\nДесеріалізовано:");
            Console.WriteLine($"Ім'я: {deserializedPlayer.Name}");
            Console.WriteLine($"Рівень (значення за замовчуванням): {deserializedPlayer.Level}");
            Console.WriteLine($"Кількість речей в інвентарі (захист від null): {deserializedPlayer.Inventory.Items.Count}");
        }

        static void RunTask7()
        {
            Console.WriteLine("Демонстрація версійності (Завдання 7) проведена в Завданні 6.");
            Console.WriteLine("Завдяки `public int Level { get; set; } = 1;` старі файли не ламають програму.");
        }
        #endregion

        #region Завдання 8: Обробка помилок десеріалізації
        static void RunTask8()
        {
            string badJson = "{ \"Name\": \"Test\" \"Level\": 5 ";

            Console.WriteLine("Спроба прочитати пошкоджений JSON:");
            Console.WriteLine(badJson);

            try
            {
                var player = JsonSerializer.Deserialize<Player>(badJson);
                Console.WriteLine("Успішно (Цього не має статися).");
            }
            catch (JsonException ex)
            {
                Console.WriteLine("\n[ПОМИЛКА] Файл збереження пошкоджено!");
                Console.WriteLine($"Деталі: {ex.Message}");
                Console.WriteLine("Програма продовжує роботу. Створюється новий об'єкт за замовчуванням...");

                var fallbackPlayer = new Player { Name = "NewPlayer" };
                Console.WriteLine($"Створено гравця: {fallbackPlayer.Name}");
            }
        }
        #endregion
    }
}