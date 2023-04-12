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
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]/{propertyId}")]
    [ApiController]
    public class AdminPropertyImagesController : ControllerBase
    {
        private readonly IPropertyImageService _propertyImageService;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IPropertyService _propertyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Cloudinary _cloudinary;

        public AdminPropertyImagesController(IPropertyImageService propertyImageService, IOptions<CloudinarySettings> cloudinaryConfig
            , IPropertyService propertyService, IUnitOfWork unitOfWork)
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

        [HttpGet("{id}")]
        [Route("GetPropertyImage")]
        public async Task<IActionResult> GetPropertyImage(int id)
        {
            var image = await _propertyImageService.GetPropertyImageById(id);

            return Ok(image);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AddImageForProperty")]
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
                        Transformation = new Transformation().Width(1000).
                            Height(1000).Crop("fill").Gravity("face")
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
            if(image.PublicId != null)
            {
                var deleteParams = new DeletionParams(image.PublicId);
                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _propertyImageService.DeletePropertyImage(id);
                }
                if (await _unitOfWork.CommitAllAsync())
                {
                    return Ok();
                }
            }
            else
            {
                _propertyImageService.DeletePropertyImage(id);
            }

            return BadRequest("Server Error");
        }
    }
}