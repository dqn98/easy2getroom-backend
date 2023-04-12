using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class PropertyRepository : EFRepository<Property, int>, IPropertyRepository
    {
        public PropertyRepository(AppDbContext context) : base(context)
        {
        }
    }
}