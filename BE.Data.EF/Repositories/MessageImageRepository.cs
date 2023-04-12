using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class MessageImageRepository : EFRepository<MessageImage, int>, IMessageImageRepository
    {
        public MessageImageRepository(AppDbContext context) : base(context)
        {
        }
    }
}