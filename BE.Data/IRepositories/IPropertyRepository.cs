using BE.Data.Entities;
using BE.Infrastructure.Interfaces;

namespace BE.Data.IRepositories
{
    public interface IPropertyRepository : IRepository<Property, int>
    {
    }
}