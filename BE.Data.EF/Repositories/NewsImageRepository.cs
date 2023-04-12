using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class NewsImageRepository : EFRepository<NewsImage, int>, INewsImageRepository
    {
        public NewsImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}