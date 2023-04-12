using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Property;
using BE.Application.ViewModels.UpdatePropertyAdmin.Property;
using BE.Data.Entities;
using BE.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPropertyController : ControllerBase
    {
        private IPropertyService _propertyService;
        private IUnitOfWork _unitOfWork;

        public AdminPropertyController(IPropertyService propertyService, IUnitOfWork unitOfWork)
        {
            _propertyService = propertyService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("GetAllProperties")]
        public async Task<List<Property>> GetAllProperties([FromBody]GetPropertiesViewModel viewModel)
        {
            return await _propertyService.GetProperties(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("GetPropertiesUser")]
        public async Task<IActionResult> GetPropertiesUser([FromBody]GetPropertiesUserViewModel viewModel)
        {
            var properties = await _propertyService.GetPropertiesUser(viewModel);
            if (properties != null)
            {
                return Ok(properties);
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("UpdateStatus")]
        public async Task<int> UpdateStatus([FromBody]UpdateStatusViewModel viewModel)
        {
            return await _propertyService.UpdateStatus(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetPropertyById/{id}")]
        public async Task<Property> GetPropertyById(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            return property;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("DeleteProperty/{id}")]
        public async Task<bool> DeleteProperty(int id)
        {
            return await _propertyService.Delete(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("UpdateProperty")]
        public async Task<IActionResult> UpdateProperty(Property property)
        {
            try
            {
                _propertyService.Update(property);
                await _unitOfWork.CommitAllAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}