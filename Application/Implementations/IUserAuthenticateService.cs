using BE.Data.Entities;
using BE.Models;
using System;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public interface IUserAuthenticateService : IDisposable
    {
        Task<User> AuthenticateGoogleUserAsync(GoogleUserRequest request);

        Task<User> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName);
    }
}