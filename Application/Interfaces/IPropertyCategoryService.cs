using BE.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IPropertyCategoryService
    {
        PropertyCategory Add(PropertyCategory propertyCategory);

        void Update(PropertyCategory propertyCategory);

        void Delete(int id);

        Task<List<PropertyCategory>> GetAll();

        Task<List<PropertyCategory>> GetAll(string keyword);

        PropertyCategory GetById(int id);

        void Save();
    }
}