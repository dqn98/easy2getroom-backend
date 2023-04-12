using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class PropertyImageRepository : EFRepository<PropertyImage, int>, IPropertyImageRepository
    {
        public PropertyImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}