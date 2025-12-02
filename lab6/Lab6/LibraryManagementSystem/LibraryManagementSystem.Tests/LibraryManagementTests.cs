using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem;

namespace LibraryManagementSystem.Tests
{
    [TestFixture]
    public class LibraryManagementTests
    {
        private static int _totalScore = 0;
        private static int _maxScore = 100;
        private static List<string> _testResults = new List<string>();

        [SetUp]
        public void Setup()
        {
            // Виводимо поточний прогрес перед кожним тестом
            if (_testResults.Count > 0)
            {
                Console.WriteLine($"\n>>> Поточний результат: {_totalScore}/{_maxScore} балів <<<\n");
            }
        }

        [OneTimeTearDown]
        public void FinalReport()
        {
            PrintFinalReport();
        }

        private void AddScore(int points, string testName)
        {
            _totalScore += points;
            string result = $"✓ {testName}: +{points} балів";
            _testResults.Add(result);

            // Виводимо результат кожного тесту відразу
            Console.WriteLine($"[PASS] {result}");
            Console.WriteLine($"       Всього балів: {_totalScore}/{_maxScore}");
        }

        private static void PrintFinalReport()
        {
            Console.WriteLine("\n" + new string('=', 70));
            Console.WriteLine("                    ФІНАЛЬНИЙ ЗВІТ ПО ТЕСТУВАННЮ");
            Console.WriteLine(new string('=', 70));
            Console.WriteLine();

            foreach (var result in _testResults)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 70));

            double percentage = (_totalScore * 100.0 / _maxScore);
            string grade = GetGrade(percentage);

            Console.WriteLine($"ЗАГАЛЬНИЙ РЕЗУЛЬТАТ: {_totalScore}/{_maxScore} балів ({percentage:F1}%)");
            Console.WriteLine($"ОЦІНКА: {grade}");
            Console.WriteLine(new string('=', 70));
            Console.WriteLine();
        }

        private static string GetGrade(double percentage)
        {
            if (percentage >= 90) return "ВІДМІННО ⭐⭐⭐⭐⭐";
            if (percentage >= 75) return "ДОБРЕ ⭐⭐⭐⭐";
            if (percentage >= 60) return "ЗАДОВІЛЬНО ⭐⭐⭐";
            return "НЕЗАДОВІЛЬНО ⭐";
        }

        // ============================================
        // ILibraryItem Interface Tests (5 балів)
        // ============================================

        [Test, Order(1)]
        public void Test01_ILibraryItem_InterfaceExists()
        {
            // 2 бали - Перевірка існування інтерфейсу ILibraryItem
            var interfaceType = typeof(ILibraryItem);
            Assert.IsTrue(interfaceType.IsInterface, "ILibraryItem повинен бути інтерфейсом");
            AddScore(2, "Інтерфейс ILibraryItem існує");
        }

        [Test, Order(2)]
        public void Test02_ILibraryItem_HasRequiredProperties()
        {
            // 3 бали - Перевірка наявності всіх необхідних властивостей
            var interfaceType = typeof(ILibraryItem);
            var properties = interfaceType.GetProperties();

            Assert.IsTrue(properties.Any(p => p.Name == "Id" && p.PropertyType == typeof(int)),
                "Повинна бути властивість Id типу int");
            Assert.IsTrue(properties.Any(p => p.Name == "Title" && p.PropertyType == typeof(string)),
                "Повинна бути властивість Title типу string");
            Assert.IsTrue(properties.Any(p => p.Name == "Year" && p.PropertyType == typeof(int)),
                "Повинна бути властивість Year типу int");

            var methods = interfaceType.GetMethods();
            Assert.IsTrue(methods.Any(m => m.Name == "GetDisplayInfo" && m.ReturnType == typeof(string)),
                "Повинен бути метод GetDisplayInfo який повертає string");

            AddScore(3, "ILibraryItem має всі необхідні властивості та методи");
        }

        // ============================================
        // LibraryItemBase Tests (15 балів)
        // ============================================

        [Test, Order(3)]
        public void Test03_LibraryItemBase_IsAbstractAndImplementsInterface()
        {
            // 3 бали - Перевірка що клас абстрактний та імплементує інтерфейс
            var baseType = typeof(LibraryItemBase);
            Assert.IsTrue(baseType.IsAbstract, "LibraryItemBase повинен бути абстрактним класом");
            Assert.IsTrue(typeof(ILibraryItem).IsAssignableFrom(baseType),
                "LibraryItemBase повинен імплементувати ILibraryItem");
            AddScore(3, "LibraryItemBase є абстрактним та імплементує ILibraryItem");
        }

        [Test, Order(4)]
        public void Test04_LibraryItemBase_AutoIncrementId()
        {
            // 5 балів - Перевірка автоматичного інкременту Id
            var book1 = new Book("Test1", 2020, "Author1");
            var book2 = new Book("Test2", 2021, "Author2");
            var book3 = new Book("Test3", 2022, "Author3");

            Assert.AreNotEqual(book1.Id, book2.Id, "Id повинні бути унікальними");
            Assert.AreNotEqual(book2.Id, book3.Id, "Id повинні бути унікальними");
            Assert.Greater(book2.Id, book1.Id, "Id повинен збільшуватись");
            Assert.Greater(book3.Id, book2.Id, "Id повинен збільшуватись");

            AddScore(5, "Автоматичний інкремент Id працює коректно");
        }

        [Test, Order(5)]
        public void Test05_LibraryItemBase_IdIsReadOnly()
        {
            // 2 бали - Перевірка що Id тільки для читання
            var idProperty = typeof(LibraryItemBase).GetProperty("Id");
            Assert.IsNotNull(idProperty, "Властивість Id повинна існувати");
            Assert.IsNull(idProperty.SetMethod, "Id повинен бути тільки для читання (без setter)");
            AddScore(2, "Властивість Id є read-only");
        }

        [Test, Order(6)]
        public void Test06_LibraryItemBase_GetItemTypeIsAbstract()
        {
            // 3 бали - Перевірка що GetItemType є абстрактним методом
            var method = typeof(LibraryItemBase).GetMethod("GetItemType");
            Assert.IsNotNull(method, "Метод GetItemType повинен існувати");
            Assert.IsTrue(method.IsAbstract, "GetItemType повинен бути абстрактним методом");
            AddScore(3, "Метод GetItemType є абстрактним");
        }

        [Test, Order(7)]
        public void Test07_LibraryItemBase_GetDisplayInfoFormat()
        {
            // 2 бали - Перевірка формату виводу GetDisplayInfo
            var book = new Book("C#", 2025, "Test Author");
            var displayInfo = book.GetDisplayInfo();

            Assert.IsTrue(displayInfo.Contains("Book"), "Вивід повинен містити тип елемента");
            Assert.IsTrue(displayInfo.Contains($"ID: {book.Id}"), "Вивід повинен містити ID");
            Assert.IsTrue(displayInfo.Contains("Title: \"C#\""), "Вивід повинен містити Title");
            Assert.IsTrue(displayInfo.Contains("Year: 2025"), "Вивід повинен містити Year");

            AddScore(2, "Формат GetDisplayInfo відповідає вимогам");
        }

        // ============================================
        // Book Class Tests (12 балів)
        // ============================================

        [Test, Order(8)]
        public void Test08_Book_InheritsFromLibraryItemBase()
        {
            // 2 бали - Перевірка наслідування
            Assert.IsTrue(typeof(LibraryItemBase).IsAssignableFrom(typeof(Book)),
                "Book повинен наслідувати LibraryItemBase");
            AddScore(2, "Book наслідує LibraryItemBase");
        }

        [Test, Order(9)]
        public void Test09_Book_HasAuthorProperty()
        {
            // 2 бали - Перевірка наявності властивості Author
            var authorProperty = typeof(Book).GetProperty("Author");
            Assert.IsNotNull(authorProperty, "Book повинен мати властивість Author");
            Assert.AreEqual(typeof(string), authorProperty.PropertyType, "Author повинен бути типу string");
            AddScore(2, "Book має властивість Author");
        }

        [Test, Order(10)]
        public void Test10_Book_ConstructorInitializesCorrectly()
        {
            // 3 бали - Перевірка конструктора
            var book = new Book("Test Title", 2023, "Test Author");

            Assert.AreEqual("Test Title", book.Title, "Title не ініціалізовано коректно");
            Assert.AreEqual(2023, book.Year, "Year не ініціалізовано коректно");
            Assert.AreEqual("Test Author", book.Author, "Author не ініціалізовано коректно");
            Assert.Greater(book.Id, 0, "Id повинен бути більше 0");

            AddScore(3, "Конструктор Book працює коректно");
        }

        [Test, Order(11)]
        public void Test11_Book_GetItemTypeReturnsBook()
        {
            // 2 бали - Перевірка GetItemType
            var book = new Book("Test", 2020, "Author");
            Assert.AreEqual("Book", book.GetItemType(), "GetItemType повинен повертати 'Book'");
            AddScore(2, "Book.GetItemType() повертає 'Book'");
        }

        [Test, Order(12)]
        public void Test12_Book_GetDisplayInfoIncludesAuthor()
        {
            // 3 бали - Перевірка що GetDisplayInfo включає автора
            var book = new Book("Programming", 2024, "John Doe");
            var displayInfo = book.GetDisplayInfo();

            Assert.IsTrue(displayInfo.Contains("Author"), "Вивід повинен містити слово 'Author'");
            Assert.IsTrue(displayInfo.Contains("John Doe"), "Вивід повинен містити ім'я автора");

            AddScore(3, "Book.GetDisplayInfo() включає інформацію про автора");
        }

        // ============================================
        // Magazine Class Tests (12 балів)
        // ============================================

        [Test, Order(13)]
        public void Test13_Magazine_InheritsFromLibraryItemBase()
        {
            // 2 бали - Перевірка наслідування
            Assert.IsTrue(typeof(LibraryItemBase).IsAssignableFrom(typeof(Magazine)),
                "Magazine повинен наслідувати LibraryItemBase");
            AddScore(2, "Magazine наслідує LibraryItemBase");
        }

        [Test, Order(14)]
        public void Test14_Magazine_HasIssueNumberProperty()
        {
            // 2 бали - Перевірка наявності властивості IssueNumber
            var property = typeof(Magazine).GetProperty("IssueNumber");
            Assert.IsNotNull(property, "Magazine повинен мати властивість IssueNumber");
            Assert.AreEqual(typeof(int), property.PropertyType, "IssueNumber повинен бути типу int");
            AddScore(2, "Magazine має властивість IssueNumber");
        }

        [Test, Order(15)]
        public void Test15_Magazine_ConstructorInitializesCorrectly()
        {
            // 3 бали - Перевірка конструктора
            var magazine = new Magazine("Tech Monthly", 2023, 42);

            Assert.AreEqual("Tech Monthly", magazine.Title, "Title не ініціалізовано коректно");
            Assert.AreEqual(2023, magazine.Year, "Year не ініціалізовано коректно");
            Assert.AreEqual(42, magazine.IssueNumber, "IssueNumber не ініціалізовано коректно");
            Assert.Greater(magazine.Id, 0, "Id повинен бути більше 0");

            AddScore(3, "Конструктор Magazine працює коректно");
        }

        [Test, Order(16)]
        public void Test16_Magazine_GetItemTypeReturnsMagazine()
        {
            // 2 бали - Перевірка GetItemType
            var magazine = new Magazine("Test", 2020, 1);
            Assert.AreEqual("Magazine", magazine.GetItemType(), "GetItemType повинен повертати 'Magazine'");
            AddScore(2, "Magazine.GetItemType() повертає 'Magazine'");
        }

        [Test, Order(17)]
        public void Test17_Magazine_GetDisplayInfoIncludesIssueNumber()
        {
            // 3 бали - Перевірка що GetDisplayInfo включає номер випуску
            var magazine = new Magazine("Science Weekly", 2024, 15);
            var displayInfo = magazine.GetDisplayInfo();

            Assert.IsTrue(displayInfo.Contains("Issue") || displayInfo.Contains("Number"),
                "Вивід повинен містити інформацію про номер випуску");
            Assert.IsTrue(displayInfo.Contains("15"), "Вивід повинен містити номер випуску");

            AddScore(3, "Magazine.GetDisplayInfo() включає номер випуску");
        }

        // ============================================
        // LibraryCatalog<T> Tests (20 балів)
        // ============================================

        [Test, Order(18)]
        public void Test18_LibraryCatalog_IsGeneric()
        {
            // 2 бали - Перевірка що клас generic
            var catalogType = typeof(LibraryCatalog<>);
            Assert.IsTrue(catalogType.IsGenericType, "LibraryCatalog повинен бути generic класом");
            AddScore(2, "LibraryCatalog є generic класом");
        }

        [Test, Order(19)]
        public void Test19_LibraryCatalog_HasConstraint()
        {
            // 3 бали - Перевірка обмеження where T : ILibraryItem
            var catalogType = typeof(LibraryCatalog<>);
            var constraints = catalogType.GetGenericArguments()[0].GetGenericParameterConstraints();

            Assert.IsTrue(constraints.Any(c => c == typeof(ILibraryItem)),
                "LibraryCatalog повинен мати обмеження where T : ILibraryItem");
            AddScore(3, "LibraryCatalog має обмеження where T : ILibraryItem");
        }

        [Test, Order(20)]
        public void Test20_LibraryCatalog_AddItemWorks()
        {
            // 3 бали - Перевірка методу AddItem
            var catalog = new LibraryCatalog<Book>();
            var book1 = new Book("Book1", 2020, "Author1");
            var book2 = new Book("Book2", 2021, "Author2");

            catalog.AddItem(book1);
            catalog.AddItem(book2);

            var items = catalog.GetAllItems();
            Assert.AreEqual(2, items.Count, "Після додавання 2 елементів, каталог повинен містити 2 елементи");

            AddScore(3, "LibraryCatalog.AddItem() працює коректно");
        }

        [Test, Order(21)]
        public void Test21_LibraryCatalog_GetAllItemsReturnsCorrectList()
        {
            // 4 бали - Перевірка методу GetAllItems
            var catalog = new LibraryCatalog<Book>();
            var book1 = new Book("Book1", 2020, "Author1");
            var book2 = new Book("Book2", 2021, "Author2");

            catalog.AddItem(book1);
            catalog.AddItem(book2);

            var items = catalog.GetAllItems();

            Assert.AreEqual(2, items.Count, "GetAllItems повинен повертати всі додані елементи");
            Assert.IsTrue(items.Contains(book1), "Список повинен містити book1");
            Assert.IsTrue(items.Contains(book2), "Список повинен містити book2");

            AddScore(4, "LibraryCatalog.GetAllItems() повертає коректний список");
        }

        [Test, Order(22)]
        public void Test22_LibraryCatalog_GetItemByIdFindsCorrectItem()
        {
            // 4 бали - Перевірка пошуку за Id
            var catalog = new LibraryCatalog<Book>();
            var book1 = new Book("Book1", 2020, "Author1");
            var book2 = new Book("Book2", 2021, "Author2");
            var book3 = new Book("Book3", 2022, "Author3");

            catalog.AddItem(book1);
            catalog.AddItem(book2);
            catalog.AddItem(book3);

            var found = catalog.GetItemById(book2.Id);

            Assert.IsNotNull(found, "GetItemById повинен знаходити існуючий елемент");
            Assert.AreEqual(book2.Id, found.Id, "Знайдений елемент повинен мати правильний Id");
            Assert.AreEqual(book2.Title, found.Title, "Знайдений елемент повинен бути тим самим об'єктом");

            AddScore(4, "LibraryCatalog.GetItemById() знаходить правильний елемент");
        }

        [Test, Order(23)]
        public void Test23_LibraryCatalog_GetItemByIdReturnsDefaultWhenNotFound()
        {
            // 4 бали - Перевірка повернення default при відсутності елемента
            var catalog = new LibraryCatalog<Book>();
            var book = new Book("Book1", 2020, "Author1");
            catalog.AddItem(book);

            var notFound = catalog.GetItemById(99999);

            Assert.IsNull(notFound, "GetItemById повинен повертати null (default) коли елемент не знайдено");

            AddScore(4, "LibraryCatalog.GetItemById() повертає default коли елемент не знайдено");
        }

        // ============================================
        // LibraryManager Tests (31 бал)
        // ============================================

        [Test, Order(24)]
        public void Test24_LibraryManager_HasRequiredCatalogs()
        {
            // 3 бали - Перевірка наявності каталогів
            var manager = new LibraryManager();
            var managerType = typeof(LibraryManager);

            var fields = managerType.GetFields(System.Reflection.BindingFlags.NonPublic |
                                               System.Reflection.BindingFlags.Instance);

            bool hasBookCatalog = fields.Any(f => f.FieldType == typeof(LibraryCatalog<Book>));
            bool hasMagazineCatalog = fields.Any(f => f.FieldType == typeof(LibraryCatalog<Magazine>));

            Assert.IsTrue(hasBookCatalog, "LibraryManager повинен мати поле LibraryCatalog<Book>");
            Assert.IsTrue(hasMagazineCatalog, "LibraryManager повинен мати поле LibraryCatalog<Magazine>");

            AddScore(3, "LibraryManager має необхідні каталоги");
        }

        [Test, Order(25)]
        public void Test25_LibraryManager_AddItemAddsBook()
        {
            // 5 балів - Перевірка додавання книги
            var manager = new LibraryManager();
            var book = new Book("Test Book", 2023, "Test Author");

            manager.AddItem(book);

            var allItems = manager.GetAllItems();
            Assert.AreEqual(1, allItems.Count, "Після додавання 1 книги, повинен бути 1 елемент");
            Assert.IsTrue(allItems.Any(i => i is Book && i.Id == book.Id),
                "Додана книга повинна бути в списку всіх елементів");

            AddScore(5, "LibraryManager.AddItem() коректно додає книги");
        }

        [Test, Order(26)]
        public void Test26_LibraryManager_AddItemAddsMagazine()
        {
            // 5 балів - Перевірка додавання журналу
            var manager = new LibraryManager();
            var magazine = new Magazine("Test Magazine", 2023, 5);

            manager.AddItem(magazine);

            var allItems = manager.GetAllItems();
            Assert.AreEqual(1, allItems.Count, "Після додавання 1 журналу, повинен бути 1 елемент");
            Assert.IsTrue(allItems.Any(i => i is Magazine && i.Id == magazine.Id),
                "Доданий журнал повинен бути в списку всіх елементів");

            AddScore(5, "LibraryManager.AddItem() коректно додає журнали");
        }

        [Test, Order(27)]
        public void Test27_LibraryManager_GetAllItemsCombinesBothCatalogs()
        {
            // 6 балів - Перевірка об'єднання каталогів
            var manager = new LibraryManager();
            var book1 = new Book("Book1", 2020, "Author1");
            var book2 = new Book("Book2", 2021, "Author2");
            var mag1 = new Magazine("Mag1", 2022, 1);
            var mag2 = new Magazine("Mag2", 2023, 2);

            manager.AddItem(book1);
            manager.AddItem(mag1);
            manager.AddItem(book2);
            manager.AddItem(mag2);

            var allItems = manager.GetAllItems();

            Assert.AreEqual(4, allItems.Count, "GetAllItems повинен повертати всі елементи з обох каталогів");
            Assert.AreEqual(2, allItems.Count(i => i is Book), "Повинно бути 2 книги");
            Assert.AreEqual(2, allItems.Count(i => i is Magazine), "Повинно бути 2 журнали");

            AddScore(6, "LibraryManager.GetAllItems() об'єднує обидва каталоги");
        }

        [Test, Order(28)]
        public void Test28_LibraryManager_GetItemByIdFindsBook()
        {
            // 4 бали - Перевірка пошуку книги за Id
            var manager = new LibraryManager();
            var book = new Book("Test Book", 2023, "Author");
            var magazine = new Magazine("Test Mag", 2023, 1);

            manager.AddItem(book);
            manager.AddItem(magazine);

            var found = manager.GetItemById(book.Id);

            Assert.IsNotNull(found, "GetItemById повинен знаходити книгу");
            Assert.AreEqual(book.Id, found.Id, "Знайдений елемент має правильний Id");
            Assert.IsTrue(found is Book, "Знайдений елемент повинен бути типу Book");

            AddScore(4, "LibraryManager.GetItemById() знаходить книги");
        }

        [Test, Order(29)]
        public void Test29_LibraryManager_GetItemByIdFindsMagazine()
        {
            // 4 бали - Перевірка пошуку журналу за Id
            var manager = new LibraryManager();
            var book = new Book("Test Book", 2023, "Author");
            var magazine = new Magazine("Test Mag", 2023, 1);

            manager.AddItem(book);
            manager.AddItem(magazine);

            var found = manager.GetItemById(magazine.Id);

            Assert.IsNotNull(found, "GetItemById повинен знаходити журнал");
            Assert.AreEqual(magazine.Id, found.Id, "Знайдений елемент має правильний Id");
            Assert.IsTrue(found is Magazine, "Знайдений елемент повинен бути типу Magazine");

            AddScore(4, "LibraryManager.GetItemById() знаходить журнали");
        }

        [Test, Order(30)]
        public void Test30_LibraryManager_GetItemByIdReturnsNullWhenNotFound()
        {
            // 4 бали - Перевірка повернення null при відсутності
            var manager = new LibraryManager();
            var book = new Book("Test Book", 2023, "Author");
            manager.AddItem(book);

            var notFound = manager.GetItemById(99999);

            Assert.IsNull(notFound, "GetItemById повинен повертати null коли елемент не знайдено");

            AddScore(4, "LibraryManager.GetItemById() повертає null коли не знайдено");
        }

        // ============================================
        // Integration Tests (5 балів)
        // ============================================

        [Test, Order(31)]
        public void Test31_Integration_MixedItemsWorkCorrectly()
        {
            // 3 бали - Інтеграційна перевірка роботи з різними типами
            var manager = new LibraryManager();

            var book1 = new Book("Design Patterns", 1994, "Gang of Four");
            var book2 = new Book("Clean Architecture", 2017, "Robert Martin");
            var mag1 = new Magazine("IEEE Software", 2024, 3);
            var mag2 = new Magazine("Communications of the ACM", 2024, 67);

            manager.AddItem(book1);
            manager.AddItem(mag1);
            manager.AddItem(book2);
            manager.AddItem(mag2);

            var allItems = manager.GetAllItems();

            Assert.AreEqual(4, allItems.Count, "Повинно бути 4 елементи");

            // Перевірка що всі елементи мають унікальні Id
            var ids = allItems.Select(i => i.Id).ToList();
            Assert.AreEqual(4, ids.Distinct().Count(), "Всі Id повинні бути унікальними");

            // Перевірка що можна знайти кожен елемент
            foreach (var item in allItems)
            {
                var found = manager.GetItemById(item.Id);
                Assert.IsNotNull(found, $"Повинен знайти елемент з Id {item.Id}");
            }

            AddScore(3, "Інтеграційна перевірка - змішані типи працюють коректно");
        }

        [Test, Order(32)]
        public void Test32_Integration_DisplayInfoForAllItems()
        {
            // 2 бали - Перевірка що GetDisplayInfo працює для всіх типів
            var manager = new LibraryManager();

            var book = new Book("Test Book", 2023, "Author");
            var magazine = new Magazine("Test Magazine", 2023, 1);

            manager.AddItem(book);
            manager.AddItem(magazine);

            var allItems = manager.GetAllItems();

            foreach (var item in allItems)
            {
                var displayInfo = item.GetDisplayInfo();

                Assert.IsNotNull(displayInfo, "GetDisplayInfo не повинен повертати null");
                Assert.IsNotEmpty(displayInfo, "GetDisplayInfo не повинен повертати порожній рядок");
                Assert.IsTrue(displayInfo.Contains(item.Title), "DisplayInfo повинен містити Title");
                Assert.IsTrue(displayInfo.Contains(item.Year.ToString()), "DisplayInfo повинен містити Year");
                Assert.IsTrue(displayInfo.Contains(item.Id.ToString()), "DisplayInfo повинен містити Id");
            }

            AddScore(2, "GetDisplayInfo працює коректно для всіх типів");

            // Виводимо фінальний звіт після останнього тесту
            PrintFinalReport();
        }
    }
}