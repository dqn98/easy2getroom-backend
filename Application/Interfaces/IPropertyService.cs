using BE.Application.ViewModels.Admin.Property;
using BE.Application.ViewModels.UpdatePropertyAdmin.Property;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IPropertyService : IDisposable
    {
        Task<List<Property>> GetAll();

        Task<List<Property>> GetProperties(GetPropertiesViewModel viewModel);

        Task<List<Property>> GetPropertiesUser(GetPropertiesUserViewModel viewModel);

        Property GetById(int id);

        Task<Property> GetByIdAsync(int id);

        Task<int> UpdateStatus(UpdateStatusViewModel viewModel);

        Task<bool> Delete(int id);

        void Update(Property property);

        void Save();

        void AddImages(string[] imagesUrl, int propertyId);

        Property Add(Property property);

        //User GetPropertyOwnerByPropertyId(int id);

        List<Property> GetLastestProperties();

        List<Property> GetRelatedProperties(int id, int top);
    }
}