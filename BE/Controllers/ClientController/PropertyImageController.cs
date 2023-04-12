using BE.Application.Interfaces;
using BE.Data.Entities;
using BE.Helpers;
using BE.Infrastructure.Interfaces;
using BE.ViewModels.Admin.PropertyImage;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyImageController : ControllerBase
    {
        private readonly IPropertyImageService _propertyImageService;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Cloudinary _cloudinary;

        public PropertyImageController(IPropertyImageService propertyImageService, IOptions<CloudinarySettings> cloudinaryConfig,
            IPropertyService propertyService, IUnitOfWork unitOfWork)
        {
            _propertyImageService = propertyImageService;
            _cloudinaryConfig = cloudinaryConfig;
            _unitOfWork = unitOfWork;
            _propertyService = propertyService;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("{propertyId}/AddImageForProperty")]
        public async Task<IActionResult> AddImageForProperty(int propertyId, [FromForm]AddPropertyImageViewModel viewModel)
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
                        Transformation = new Transformation().Width(1366).
                            Height(905).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            viewModel.Url = uploadResult.Url.ToString();
            viewModel.PublicId = uploadResult.PublicId;

            var propertyImage = new PropertyImage();
            propertyImage.Url = viewModel.Url;
            propertyImage.PublicId = viewModel.PublicId;
            propertyImage.PropertyId = propertyId;
            _propertyImageService.AddPropertyImage(propertyImage);
            await _unitOfWork.CommitAllAsync();
            return Ok(propertyImage);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Client")]
        [Route("GetPropertyImages/{propertyId}")]
        public async Task<List<PropertyImage>> GetPropertyImages(int propertyId)
        {
            return await _propertyImageService.GetPropertyImages(propertyId);
        }
    }
}