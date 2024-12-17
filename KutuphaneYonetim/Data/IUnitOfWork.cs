using System;
using System.Threading.Tasks;

namespace KutuphaneYonetim.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        Task<int> CompleteAsync();
    }
}