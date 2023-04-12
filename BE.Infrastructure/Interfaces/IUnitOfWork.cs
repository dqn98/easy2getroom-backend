using System;
using System.Threading.Tasks;

namespace BE.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Call save change for dbcontext
        /// </summary>
        void Commit();

        Task<int> CommitAsync();

        Task<bool> CommitAllAsync();
    }
}