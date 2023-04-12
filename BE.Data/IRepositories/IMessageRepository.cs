using BE.Data.Entities;
using BE.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace BE.Data.IRepositories
{
    public interface IMessageRepository : IRepository<Message, int>
    {
    }
}