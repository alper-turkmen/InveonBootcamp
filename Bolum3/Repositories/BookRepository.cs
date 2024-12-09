using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bolum3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bolum3.Repositories
{
    public class BookRepository : IBookRepository
    {
        private static List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "Kitap 1", Author = "Yazar 1" },
            new Book { Id = 2, Title = "Kitap 2", Author = "Yazar 2" },
            new Book { Id = 3, Title = "Kitap 3", Author = "Yazar 3" },
            new Book { Id = 4, Title = "Kitap 4", Author = "Yazar 4" },
            new Book { Id = 5, Title = "Kitap 5", Author = "Yazar 5" },
            new Book { Id = 6, Title = "Kitap 6", Author = "Yazar 6" },
            new Book { Id = 7, Title = "Kitap 7", Author = "Yazar 7" },
            new Book { Id = 8, Title = "Kitap 8", Author = "Yazar 8" },
            new Book { Id = 9, Title = "Kitap 9", Author = "Yazar 9" },
            new Book { Id = 10, Title = "Kitap 10", Author = "Yazar 9" },
            new Book { Id = 11, Title = "Kitap 11", Author = "Yazar 10" },
            new Book { Id = 12, Title = "Kitap 12", Author = "Yazar 11" },
            new Book { Id = 13, Title = "Kitap 13", Author = "Yazar 12" }
        };

        public List<Book> GetAll() => _books;

        public Book GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

        public void Add(Book book)
        {
            book.Id = _books.Max(b => b.Id) + 1;
            _books.Add(book);
        }

        public void Update(Book book)
        {
            var existingBook = GetById(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
            }
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}