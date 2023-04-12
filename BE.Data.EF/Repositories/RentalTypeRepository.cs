using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class RentalTypeRepository : EFRepository<RentalType, int>, IRentalTypeRepository
    {
        public RentalTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}