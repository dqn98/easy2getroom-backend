using BE.Data.Entities;
using BE.Data.IRepositories;

namespace BE.Data.EF.Repositories
{
    public class MessageRepository : EFRepository<Message, int>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
    }
}