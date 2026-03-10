using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // ПІБ: Грищенко Владислав Олегович
            // Група: ПД-24
        }
    }

    public interface ILibraryItem
    {
        int Id { get; }
        string Title { get; }
        int Year { get; }
        string GetDisplayInfo();
    }

    public abstract class LibraryItemBase : ILibraryItem
    {
        private static int _nextId = 1;

        public int Id { get; }
        public string Title { get; set; }
        public int Year { get; set; }

        public LibraryItemBase(string title, int year)
        {
            Id = _nextId++;
            Title = title;
            Year = year;
        }

        public abstract string GetItemType();

        public virtual string GetDisplayInfo()
        {
            return $"{GetItemType()} ID: {Id}, Title: \"{Title}\", Year: {Year}";
        }
    }

    public class Book : LibraryItemBase
    {
        public string Author { get; set; }

        public Book(string title, int year, string author) : base(title, year)
        {
            Author = author;
        }

        public override string GetItemType()
        {
            return "Book";
        }

        public override string GetDisplayInfo()
        {
            return $"{base.GetDisplayInfo()}, Author: {Author}";
        }
    }

    public class Magazine : LibraryItemBase
    {
        public int IssueNumber { get; set; }

        public Magazine(string title, int year, int issueNumber) : base(title, year)
        {
            IssueNumber = issueNumber;
        }

        public override string GetItemType()
        {
            return "Magazine";
        }

        public override string GetDisplayInfo()
        {
            return $"{base.GetDisplayInfo()}, Issue Number: {IssueNumber}";
        }
    }

    public class LibraryCatalog<T> where T : ILibraryItem
    {
        private List<T> _items = new List<T>();

        public void AddItem(T item)
        {
            _items.Add(item);
        }

        public List<T> GetAllItems()
        {
            return new List<T>(_items);
        }

        public T GetItemById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }
    }

    public class LibraryManager
    {
        private LibraryCatalog<Book> _bookCatalog;
        private LibraryCatalog<Magazine> _magazineCatalog;

        public LibraryManager()
        {
            _bookCatalog = new LibraryCatalog<Book>();
            _magazineCatalog = new LibraryCatalog<Magazine>();
        }

        public void AddItem(ILibraryItem item)
        {
            if (item is Book book)
            {
                _bookCatalog.AddItem(book);
            }
            else if (item is Magazine magazine)
            {
                _magazineCatalog.AddItem(magazine);
            }
        }

        public List<ILibraryItem> GetAllItems()
        {
            List<ILibraryItem> result = new List<ILibraryItem>();
            result.AddRange(_bookCatalog.GetAllItems());
            result.AddRange(_magazineCatalog.GetAllItems());
            return result;
        }

        public ILibraryItem? GetItemById(int id)
        {
            var book = _bookCatalog.GetItemById(id);
            if (book != null)
            {
                return book;
            }

            var magazine = _magazineCatalog.GetItemById(id);
            if (magazine != null)
            {
                return magazine;
            }

            return null;
        }
    }
}