using BE.Application.ViewModels.Client;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IClientUserService : IDisposable
    {
        Task<ClientUserViewModel> GetUser(string username);

        Task<List<ClientUserViewModel>> GetUsers();

        Task<ClientUserViewModel> MappingUserAsync(User user);
    }
}