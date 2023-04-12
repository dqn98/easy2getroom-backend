using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.User;
using BE.Data.Entities;
using BE.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private IUnitOfWork _unitOfWork;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<UserResultViewModel>> GetUsers(GetUserViewModel viewModel)
        {
            var users = await _userManager.Users.Where(u => (viewModel.Keyword == "" ? true : u.FullName.Contains(viewModel.Keyword)
                 || u.Email.Contains(viewModel.Keyword))).ToListAsync();

            var userResults = new List<UserResultViewModel>();

            if (users.Count != 0)
            {
                foreach (User user in users)
                {
                    var userResult = new UserResultViewModel();
                    if (viewModel.Role != "")
                    {
                        var role = await _userManager.GetRolesAsync(user);
                        if (role.FirstOrDefault().Equals(viewModel.Role))
                        {
                            userResult.FullName = user.FullName;
                            userResult.Avatar = user.Avatar;
                            userResult.BirthDay = user.BirthDay;
                            userResult.DateCreated = user.DateCreated;
                            userResult.DateModified = user.DateModified;
                            userResult.Id = user.Id;
                            userResult.Username = user.UserName;
                            userResult.Email = user.Email;
                            userResult.RoleName = role.FirstOrDefault();
                            userResult.FacebookUrl = user.FacebookUrl;
                            userResult.TwitterUrl = user.TwitterUrl;
                            userResult.WebsiteUrl = user.WebsiteUrl;

                            userResults.Add(userResult);
                        }
                    }
                    else
                    {
                        var role = await _userManager.GetRolesAsync(user);
                        userResult.FullName = user.FullName;
                        userResult.Avatar = user.Avatar;
                        userResult.BirthDay = user.BirthDay;
                        userResult.DateCreated = user.DateCreated;
                        userResult.DateModified = user.DateModified;
                        userResult.Id = user.Id;
                        userResult.Email = user.Email;
                        userResult.Username = user.UserName;
                        userResult.RoleName = role.FirstOrDefault();
                        userResult.FacebookUrl = user.FacebookUrl;
                        userResult.TwitterUrl = user.TwitterUrl;
                        userResult.WebsiteUrl = user.WebsiteUrl;
                        userResults.Add(userResult);
                    }
                }
            }
            return userResults;
        }

        public async Task<UserResultViewModel> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var viewModel = new UserResultViewModel();

            viewModel.Avatar = user.Avatar;
            viewModel.BirthDay = user.BirthDay;
            viewModel.DateCreated = user.DateCreated;
            viewModel.DateModified = user.DateModified;
            viewModel.FullName = user.FullName;
            viewModel.Email = user.Email;
            viewModel.Username = user.UserName;
            viewModel.Id = user.Id;
            viewModel.FacebookUrl = user.FacebookUrl;
            viewModel.TwitterUrl = user.TwitterUrl;
            viewModel.WebsiteUrl = user.WebsiteUrl;

            var role = await _userManager.GetRolesAsync(user);
            viewModel.RoleName = role.FirstOrDefault();

            return viewModel;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<bool> Update(UpdateUserViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id.ToString());
            user.FullName = viewModel.FullName;
            user.BirthDay = viewModel.BirthDay;
            var updateUser = await _userManager.UpdateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();
            var updateRole = await _userManager.RemoveFromRoleAsync(user, role);
            if (updateRole.Succeeded && updateUser.Succeeded)
            {
                return await _unitOfWork.CommitAllAsync();
            }
            _unitOfWork.Dispose();
            return false;
        }

        public async Task<bool> Update(User user)
        {
            var updateUser = await _userManager.UpdateAsync(user);
            if (updateUser.Succeeded)
            {
                return await _unitOfWork.CommitAllAsync();
            }
            _unitOfWork.Dispose();
            return false;
        }

        public async Task<bool> UpdateRole(UpdateRoleViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null && roles != null)
            {
                var removeRole = await _userManager.RemoveFromRolesAsync(user, roles.ToList());
                if (removeRole.Succeeded)
                {
                    var addRo1e = await _userManager.AddToRoleAsync(user, viewModel.Role);
                    if (addRo1e.Succeeded)
                    {
                        await _unitOfWork.CommitAsync();
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }
    }
}