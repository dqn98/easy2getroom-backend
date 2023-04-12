using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Client.Property;
using BE.Application.ViewModels.Client.Rating;
using BE.Application.ViewModels.Client.User;
using BE.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IPropertyImageService _propertyImageService;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public AccountController(IAccountService accountService,
            IPropertyImageService propertyImageService,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _accountService = accountService;
            _cloudinaryConfig = cloudinaryConfig;
            _propertyImageService = propertyImageService;
            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Client")]
        [Route("GetUserProperties/{userId}")]
        public Task<List<PropertyVM>> GetUserProperties(Guid userId)
        {
            return _accountService.GetUserProperties(userId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("GetFavorites")]
        public Task<List<PropertyVM>> GetFavorites(FavoriteViewModel viewModel)
        {
            return _accountService.GetFavorites(viewModel.UserId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("Favorite")]
        public async Task<IActionResult> Favorite(FavoriteViewModel viewModel)
        {
            var result = await _accountService.Favorite(viewModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("Unfavorite")]
        public async Task<IActionResult> Unfavorite(FavoriteViewModel viewModel)
        {
            var result = await _accountService.Unfavorite(viewModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("Rating")]
        public async Task<IActionResult> Rating(RatingViewModel viewModel)
        {
            var result = await _accountService.Rating(viewModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("CheckRating")]
        public async Task<int> CheckRating(CheckRatingViewModel viewModel)
        {
            return await _accountService.CheckRating(viewModel);
        }

        //Client logged in
        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("GetMyProperties")]
        public Task<List<PropertyVM>> GetMyProperties(GetUserPropertiesViewModel viewModel)
        {
            if (viewModel.UserId != null)
            {
                return _accountService.GetMyProperties(viewModel);
            }
            return null;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("SubmitProperty")]
        public async Task<int> SubmitProperty(SubmitPropertyViewModel viewModel)
        {
            return await _accountService.SubmitProperty(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("AddPropertyFeatures")]
        public async Task<bool> AddPropertyFeatures(AddPropertyFeaturesViewModel viewModel)
        {
            return await _accountService.AddPropertyFeatures(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("UpdateStatus")]
        public async Task<bool> UpdateStatus([FromBody]ClientUpdateStatusViewModel viewModel)
        {
            return await _accountService.UpdateStatus(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("DeleteProperty")]
        public async Task<bool> DeleteProperty([FromBody]DeletePropertyViewModel viewModel)
        {
            return await _accountService.DeleteProperty(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Client")]
        [Route("GetProfile/{username}")]
        public async Task<ProfileViewModel> GetProfile(string username)
        {
            return await _accountService.GetProfile(username);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel viewModel)
        {
            var result = await _accountService.UpdateProfile(viewModel);
            if (result == true)
            {
                return Ok(true);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("{id}/UpdateAvatar")]
        public async Task<IActionResult> UpdateAvatar(string id)
        {
            var file = Request.Form.Files[0];
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

            var result = await _accountService.UpdateAvatar(id, uploadResult.Url.ToString(), uploadResult.PublicId.ToString());
            if (result == true)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("UpdateBasicForm/{propertyId}")]
        public async Task<bool> UpdateBasicForm(UpdateBasicFormViewModel viewModel, int propertyId)
        {
            return await _accountService.UpdateBasicForm(viewModel, propertyId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("UpdateAddressForm/{propertyId}")]
        public async Task<bool> UpdateAddressForm(UpdateAddressFormViewModel viewModel, int propertyId)
        {
            return await _accountService.UpdateAddressForm(viewModel, propertyId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("UpdateAdditionalForm/{propertyId}")]
        public async Task<bool> UpdateAdditionalForm(UpdateAdditionalFormViewModel viewModel, int propertyId)
        {
            return await _accountService.UpdateAdditionalForm(viewModel, propertyId);
        }

        [HttpGet("id")]
        [Authorize(Roles = "Admin")]
        [Route("DeleteImageForProperty/{id}")]
        public async Task<IActionResult> DeleteImageForProperty(int id)
        {
            var image = await _propertyImageService.GetPropertyImageById(id);
            if (image == null)
            {
                return BadRequest("Server Error");
            }
            if (image.PublicId != null)
            {
                var deleteParams = new DeletionParams(image.PublicId);
                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    var isDelete = await _propertyImageService.DeletePropertyImageClient(id);
                    if (isDelete)
                    {
                        return Ok(isDelete);
                    }
                    return BadRequest();
                }
            }
            else
            {
                var isDelete = await _propertyImageService.DeletePropertyImageClient(id);
                if (isDelete)
                {
                    return Ok(isDelete);
                }
                return BadRequest();
            }

            return BadRequest("Server Error");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("AddPropertyImages/{propertyId}")]
        public async Task<IActionResult> AddPropertyImages(int propertyId)
        {
            var files = Request.Form.Files;
            var result = false;
            foreach(IFormFile file in files)
            {
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
                        result = await _accountService.AddPropertyImages(propertyId, uploadResult.Url.ToString(), uploadResult.PublicId.ToString());
                    }
                }
            }
  
            if (result == true)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}