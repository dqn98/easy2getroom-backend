using BE.Data.Entities;
using BE.Ultilities.Constants;
using BE.ViewModals.Shared;
using BE.ViewModels.Admin.User;
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

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminSecurityController : ControllerBase
    {
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AdminSecurityController(UserManager<User> userManager, IConfiguration configuration)
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
                if (role.FirstOrDefault().Equals(CommonConstants.AdminRole))
                {
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
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}