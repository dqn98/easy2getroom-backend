using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.User;
using BE.Helpers;
using BE.Infrastructure.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private IUserService _userService;
        private IUnitOfWork _unitOfWork;
        private readonly Cloudinary _cloudinary;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;

        public AdminUserController(IUserService userService, IUnitOfWork unitOfWork,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("GetUsers")]
        public async Task<List<UserResultViewModel>> GetUsers(GetUserViewModel viewModel)
        {
            return await _userService.GetUsers(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetUser/{id}")]
        public async Task<UserResultViewModel> GetUser(string id)
        {
            return await _userService.GetUser(id);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateUserViewModel viewModel)
        {
            var result = await _userService.Update(viewModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("UpdateRole")]
        public async Task<IActionResult> UpdateRole(UpdateRoleViewModel viewModel)
        {
            var result = await _userService.UpdateRole(viewModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AddAvatar/{id}")]
        public async Task<IActionResult> AddAvatar(string id,[FromForm]AddAvatarViewModel viewModel)
        {
            var file = viewModel.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(1000).
                            Height(1000).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            var userId = new Guid(id);
            var user = await _userService.GetUser(userId);
            user.Avatar = uploadResult.Url.ToString();
            user.AvatarPublicId = uploadResult.PublicId;
            await _userService.Update(user);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("DeleteAvatar/{id}")]
        public async Task<IActionResult> DeleteAvatar(string id)
        {
            var userId = new Guid(id);
            var user = await _userService.GetUser(userId);

            if(user.AvatarPublicId != null)
            {
                var deleteParams = new DeletionParams(user.AvatarPublicId);
                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    user.Avatar = "Default";
                    user.AvatarPublicId = "Default";
                }
                if(await _unitOfWork.CommitAllAsync())
                {
                    return Ok();
                }
            }
            return BadRequest("Server Error");
        }
    }
}