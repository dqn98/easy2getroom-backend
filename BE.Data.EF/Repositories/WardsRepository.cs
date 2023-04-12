using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class WardsRepository : EFRepository<Wards, int>, IWardsRepository
    {
        public WardsRepository(AppDbContext context) : base(context)
        {
        }
    }
}