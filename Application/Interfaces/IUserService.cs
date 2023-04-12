using BE.Application.ViewModels.Admin.User;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<List<UserResultViewModel>> GetUsers(GetUserViewModel viewModel);

        Task<UserResultViewModel> GetUser(string id);

        Task<User> GetUser(Guid id);

        Task<bool> Delete(Guid id);

        Task<bool> Update(UpdateUserViewModel viewModel);

        Task<bool> Update(User user);

        Task<bool> UpdateRole(UpdateRoleViewModel viewModel);
    }
}