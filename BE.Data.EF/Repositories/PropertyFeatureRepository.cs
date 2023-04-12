using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class PropertyFeatureRepository : EFRepository<PropertyFeature>, IPropertyFeatureRepository
    {
        public PropertyFeatureRepository(AppDbContext context) : base(context)
        {
        }
    }
}