using BE.Application.ViewModels.Client;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IClientPropertyService : IDisposable
    {
        Task<List<PropertyVM>> GetProperties();

        Task<PropertyVM> GetPropertyById(int id);

        Task<PropertyVM> GetPropertyByIdToEdit(int id);

        Task<List<PropertyVM>> GetRelatedProperties(int id);

        PropertyVM MappingData(Property p);

        Task<List<PropertyVM>> GetFeaturedProperties();

        Task<ClientUserViewModel> GetPropertyOwner(int propertyId);

        Task<bool> AddViewCount(int propertyId);

        ClientUserViewModel MappingUser(User user);

        Task<List<PropertyVM>> GetPropertiesByUsername(string username);

    }
}