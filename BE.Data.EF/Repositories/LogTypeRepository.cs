using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class LogTypeRepository : EFRepository<LogType, int>, ILogTypeRepository
    {
        public LogTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}