using Bolum3.Models;
using Bolum3.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolum3.Services
{
    public class BookServiceNoRedis : IBookService
    {
        private readonly IBookRepository _repository;

        public BookServiceNoRedis(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task<ServiceResult<List<Book>>> GetAllBooksAsync()
        {
            
            var books = _repository.GetAll();
            return Task.FromResult(ServiceResult<List<Book>>.Success(books, 200));
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

        public Task<ServiceResult> CreateBookAsync(Book book)
        {
            _repository.Add(book);
            return Task.FromResult(ServiceResult.Success(201));
        }

        public Task<ServiceResult> UpdateBookAsync(Book book)
        {
            var existingBook = _repository.GetById(book.Id);
            if (existingBook == null)
                return Task.FromResult(ServiceResult.Fail("Book not found", 404));

            _repository.Update(book);
            return Task.FromResult(ServiceResult.Success(204));
        }

        public Task<ServiceResult> DeleteBookAsync(int id)
        {
            var book = _repository.GetById(id);
            if (book == null)
                return Task.FromResult(ServiceResult.Fail("Book not found", 404));

            _repository.Delete(id);
            return Task.FromResult(ServiceResult.Success(204));
        }
    }
}