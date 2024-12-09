using Bolum3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bolum3.Services
{
    public interface IBookService
    {
        Task<ServiceResult<List<Book>>> GetAllBooksAsync();
        ServiceResult<Book> GetBookById(int id);
        Task<ServiceResult> CreateBookAsync(Book book);
        Task<ServiceResult> UpdateBookAsync(Book book);
        Task<ServiceResult> DeleteBookAsync(int id);
        Task<ServiceResult<List<Book>>> GetBooksPagedAsync(int page, int pageSize);
    }
}