using BE.Data.Entities;
using BE.Infrastructure.Interfaces;

namespace BE.Data.IRepositories
{
    public interface ILoggingRepository : IRepository<Log, int>
    {
    }
}