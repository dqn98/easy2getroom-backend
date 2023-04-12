using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
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
    public class ClientUserService : IClientUserService
    {
        private UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;

        public ClientUserService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ClientUserViewModel> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return await MappingUserAsync(user);
        }

        public async Task<List<ClientUserViewModel>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userResults = new List<ClientUserViewModel>();

            if (users.Count != 0)
            {
                foreach (User user in users)
                {
                    var userResult = await MappingUserAsync(user);
                    userResults.Add(userResult);
                }
            }
            return userResults;
        }

        public async Task<ClientUserViewModel> MappingUserAsync(User user)
        {
            var role = await _userManager.GetRolesAsync(user);
            
            var vm = new ClientUserViewModel();
            vm.Id = user.Id;
            vm.Username = user.UserName;
            vm.FullName = user.FullName;
            vm.Desc = "Address: " + user.Address + ", Date registed: " + user.DateCreated.ToString();
            vm.Organization = "Easy2GetRoom";
            vm.Email = user.Email;
            vm.Phone = user.PhoneNumber;

            vm.Social = new Social();
            vm.Social.Facebook = user.FacebookUrl;
            vm.Social.Twitter = user.TwitterUrl;
            vm.Social.Website = user.WebsiteUrl;
            vm.Social.Instagram = "";
            vm.Social.Linkedin = "";
            vm.RoleName = role.FirstOrDefault();
            vm.Image = user.Avatar;

            return vm;
        }
    }
}