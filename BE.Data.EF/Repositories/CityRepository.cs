using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class CityRepository : EFRepository<City, int>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }
    }
}