using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IPropertyImageService : IDisposable
    {
        List<PropertyImage> GetPropertyImageByPropertyId(int id);

        Task<List<PropertyImage>> GetPropertyImages(int propertyId);

        Task<int> SaveAsync(PropertyImage propertyImage);

        Task<PropertyImage> GetPropertyImageById(int id);

        void AddPropertyImage(PropertyImage propertyImage);

        void DeletePropertyImage(int id);

        Task<bool> DeletePropertyImageClient(int propertyId);
    }
}