using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bolum3.Models;
using System.Collections.Generic;

namespace Bolum3.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAll();

        Book GetById(int id);

        void Add(Book book);

        void Update(Book book);
        
        void Delete(int id);

    }
}