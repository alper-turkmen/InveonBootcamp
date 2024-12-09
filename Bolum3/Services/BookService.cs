using Bolum3.Models;
using Bolum3.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolum3.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IDistributedCache _cache;


        public BookService(IBookRepository repository, IDistributedCache cache)
        {
            
            _repository = repository;
            _cache = cache;
        }

        public async Task<ServiceResult<List<Book>>> GetAllBooksAsync()
        {

            const string cacheKey = "AllBooks";

            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                Console.WriteLine("Data from cache");
                var booksFromCache = JsonSerializer.Deserialize<List<Book>>(cachedData);
                return ServiceResult<List<Book>>.Success(booksFromCache, 200);
            }

            var books = _repository.GetAll();
            Console.WriteLine("Data from repo");
            var serializedData = JsonSerializer.Serialize(books);

            await _cache.SetStringAsync(cacheKey, serializedData);

            return ServiceResult<List<Book>>.Success(books, 200);
        }

        public async Task<ServiceResult<List<Book>>> GetBooksPagedAsync(int page, int pageSize)
        {
            var allBooks = await GetAllBooksAsync();
            var books = allBooks.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return ServiceResult<List<Book>>.Success(books, 200);
        }

        public ServiceResult<Book> GetBookById(int id)
        {
            var book = _repository.GetById(id);
            if (book == null)
                return ServiceResult<Book>.Fail("Book not found", 404);

            return ServiceResult<Book>.Success(book, 200);
        }

        public async Task<ServiceResult> CreateBookAsync(Book book)
        {
            _repository.Add(book);

            await _cache.RemoveAsync("AllBooks");

            return ServiceResult.Success(201);
        }

        public async Task<ServiceResult> UpdateBookAsync(Book book)
        {
            var existingBook = _repository.GetById(book.Id);
            if (existingBook == null)
                return ServiceResult.Fail("Book not found", 404);

            _repository.Update(book);

            await _cache.RemoveAsync("AllBooks");

            return ServiceResult.Success(204);
        }

        public async Task<ServiceResult> DeleteBookAsync(int id)
        {
            var book = _repository.GetById(id);
            if (book == null)
                return ServiceResult.Fail("Book not found", 404);

            _repository.Delete(id);

            await _cache.RemoveAsync("AllBooks");

            return ServiceResult.Success(204);
        }
    }
}