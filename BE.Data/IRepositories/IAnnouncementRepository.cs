using BE.Data.Entities;
using BE.Infrastructure.Interfaces;

namespace BE.Data.IRepositories
{
    public interface IAnnouncementRepository : IRepository<Announcement, int>
    {
    }
}