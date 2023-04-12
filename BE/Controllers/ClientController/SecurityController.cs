using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Shared;
using BE.Data.Entities;
using BE.Ultilities.Constants;
using BE.ViewModals.Shared;
using BE.ViewModels.Admin.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public SecurityController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var userFromDb = await _userManager.FindByEmailAsync(viewModel.Email);
            if (userFromDb != null && await _userManager.CheckPasswordAsync(userFromDb, viewModel.Password))
            {
                var role = await _userManager.GetRolesAsync(userFromDb);
                IdentityOptions _options = new IdentityOptions();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("user_id", userFromDb.Id.ToString()),
                        new Claim("user_name", userFromDb.UserName.ToString()),
                        new Claim("email", userFromDb.Email.ToString()),
                        new Claim("full_name", userFromDb.FullName.ToString()),
                        new Claim("photo_url", userFromDb.Avatar!=null ? userFromDb.Avatar.ToString() : ""),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSetting:JWT_Secret"])),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                var user = new UserViewModel();

                user.FullName = userFromDb.FullName;
                user.UserName = userFromDb.UserName;
                user.Email = userFromDb.Email;
                user.BirthDay = userFromDb.BirthDay;
                user.Avatar = userFromDb.Avatar;
                user.DateCreated = userFromDb.DateCreated;
                user.DateModified = userFromDb.DateModified;

                return Ok(new { token, user });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (await _userManager.FindByEmailAsync(viewModel.Email) != null)
            {
                // User is existed
                return BadRequest("Email is existed");
            }
            var userName = viewModel.Email.Substring(0, viewModel.Email.LastIndexOf("@"));
            if (viewModel.Avatar == null)
            {
                viewModel.Avatar = CommonConstants.DefaultAvatar;
            }
            var user = new User()
            {
                UserName = userName,
                FullName = viewModel.FullName,
                Email = viewModel.Email,
                Avatar = viewModel.Avatar,
                BirthDay = viewModel.BirthDay,
                PhoneNumber = viewModel.PhoneNumber,
                Address = viewModel.Address,
                AvatarPublicId = viewModel.AvatarPublicId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            };

            var createUser = await _userManager.CreateAsync(user, viewModel.Password);
            if (createUser.Succeeded)
            {
                var userFromDb = await _userManager.FindByEmailAsync(user.Email);
                if (userFromDb != null)
                {
                    await _userManager.AddToRoleAsync(userFromDb, CommonConstants.ClientRole);
                    return Ok(user);
                }
            }
            return BadRequest("Register failed");
        }

        [HttpPost]
        [Route("externalLogin")]
        public async Task<IActionResult> ExternalLogin(ExternalLoginViewModel viewModel)
        {
            if (viewModel.Email == null)
            {
                return BadRequest(null);
            }

            var userFromDb = await _userManager.FindByEmailAsync(viewModel.Email);
            if (userFromDb != null)
            {
                var role = await _userManager.GetRolesAsync(userFromDb);
                IdentityOptions _options = new IdentityOptions();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("user_id", userFromDb.Id.ToString()),
                        new Claim("user_name", userFromDb.UserName.ToString()),
                        new Claim("email", userFromDb.Email.ToString()),
                        new Claim("full_name", userFromDb.FullName.ToString()),
                        new Claim("photo_url", userFromDb.Avatar!=null ? userFromDb.Avatar.ToString() : ""),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSetting:JWT_Secret"])),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                var user = new UserViewModel();

                user.FullName = userFromDb.FullName;
                user.UserName = userFromDb.UserName;
                user.Email = userFromDb.Email;
                user.BirthDay = userFromDb.BirthDay;
                user.Avatar = userFromDb.Avatar;
                user.DateCreated = userFromDb.DateCreated;
                user.DateModified = userFromDb.DateModified;

                return Ok(new { token, user });
            }
            else
            {
                var userName = viewModel.Email.Substring(0, viewModel.Email.LastIndexOf("@"));
                var user = new User()
                {
                    UserName = userName,
                    FullName = viewModel.Name,
                    Email = viewModel.Email,
                    Avatar = viewModel.PhotoUrl,
                    BirthDay = viewModel.BirthDay,
                    PhoneNumber = viewModel.PhoneNumber,
                    Address = viewModel.Address,
                    AvatarPublicId = viewModel.PhotoUrl,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                };

                if (viewModel.PhotoUrl == null)
                {
                    viewModel.PhotoUrl = CommonConstants.DefaultAvatar;
                }

                var createUser = await _userManager.CreateAsync(user);
                if (createUser.Succeeded)
                {
                    var newUserFromDb = await _userManager.FindByEmailAsync(user.Email);
                    if (newUserFromDb != null)
                    {
                        var addRole = await _userManager.AddToRoleAsync(newUserFromDb, CommonConstants.ClientRole);
                        if (addRole.Succeeded)
                        {
                            var roles = await _userManager.GetRolesAsync(newUserFromDb);
                            var role = roles.FirstOrDefault();
                            IdentityOptions _options = new IdentityOptions();
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                    new Claim("user_id", newUserFromDb.Id.ToString()),
                                    new Claim("user_name", newUserFromDb.UserName.ToString()),
                                    new Claim("email", newUserFromDb.Email.ToString()),
                                    new Claim("full_name", newUserFromDb.FullName.ToString()),
                                    new Claim("photo_url", newUserFromDb.Avatar!=null ? newUserFromDb.Avatar.ToString() : ""),
                                    new Claim(_options.ClaimsIdentity.RoleClaimType, role)
                                }),
                                Expires = DateTime.UtcNow.AddHours(2),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSetting:JWT_Secret"])),
                                SecurityAlgorithms.HmacSha256Signature)
                            };
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                            var token = tokenHandler.WriteToken(securityToken);
                            var userVM = new UserViewModel();

                            userVM.FullName = newUserFromDb.FullName;
                            userVM.UserName = newUserFromDb.UserName;
                            userVM.Email = newUserFromDb.Email;
                            userVM.BirthDay = newUserFromDb.BirthDay;
                            userVM.Avatar = newUserFromDb.Avatar;
                            userVM.DateCreated = newUserFromDb.DateCreated;
                            userVM.DateModified = newUserFromDb.DateModified;

                            return Ok(new { token, userVM });
                        }
                    }
                }
                return BadRequest("Register failed");
            }
        }

        [HttpPost]
        [Route("IsEmailExisted")]
        public async Task<bool> IsEmailExisted(SocialUser socialUser)
        {
            var userFromDb = await _userManager.FindByEmailAsync(socialUser.Email);
            if (userFromDb == null)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize(Roles = "Admin, Client")]
        public async Task<bool> ChangePassword(ChangePasswordViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.UserId);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);
                if(result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }
    }
}