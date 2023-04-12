using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class PropertyCategoryRepository : EFRepository<PropertyCategory, int>, IPropertyCategoryRepository
    {
        public PropertyCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}