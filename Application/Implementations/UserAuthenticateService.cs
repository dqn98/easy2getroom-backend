using BE.Application.Helpers;
using BE.Data.Entities;
using BE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace BE.Application.Implementations
{
    public class UserAuthenticateService : IUserAuthenticateService
    {
        private UserManager<User> _userManager;
        private readonly IOptions<GoogleAuthenticationSetting> _googleAuthenticationSetting;

        public UserAuthenticateService(UserManager<User> userManager, IOptions<GoogleAuthenticationSetting> googleAuthenticationSetting)
        {
            _userManager = userManager;
            _googleAuthenticationSetting = googleAuthenticationSetting;
        }

        public async Task<User> AuthenticateGoogleUserAsync(GoogleUserRequest request)
        {
            Payload payload = await ValidateAsync(request.IdToken, new ValidationSettings
            {
                Audience = new[] { _googleAuthenticationSetting.Value.ClientId}
            });

            return await GetOrCreateExternalLoginUser(GoogleUserRequest.PROVIDER, payload.Subject, payload.Email, payload.GivenName, payload.FamilyName);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<User> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName)
        {
            var user = await _userManager.FindByLoginAsync(provider, key);
            if (user != null)
                return user;
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    UserName = email,
                    FullName = firstName + lastName,
                    Id = Guid.Parse(key),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                };
                await _userManager.CreateAsync(user);
            }

            var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());
            var result = await _userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
                return user;
            return null;
        }
    }
}