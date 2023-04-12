using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class LoggingRepository : EFRepository<Log, int>, ILoggingRepository
    {
        public LoggingRepository(AppDbContext context) : base(context)
        {
        }
    }
}