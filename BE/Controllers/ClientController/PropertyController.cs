using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private IClientPropertyService _clientPropertyService;
        private IPropertyCategoryService _properyCategoryService;

        public PropertyController(IClientPropertyService clientPropertyService, IPropertyCategoryService properyCategoryService)
        {
            _clientPropertyService = clientPropertyService;
            _properyCategoryService = properyCategoryService;
        }

        [HttpGet]
        [Route("GetProperties")]
        public Task<List<PropertyVM>> GetProperties()
        {
            return _clientPropertyService.GetProperties();
        }

        [HttpGet]
        [Route("GetPropertyById/{id}")]
        public async Task<PropertyVM> GetPropertyById(int id)
        {
            if (await this._clientPropertyService.AddViewCount(id))
            {
                return await _clientPropertyService.GetPropertyById(id);
            }

            return null;
        }

        [HttpGet]
        [Route("GetPropertyByIdToEdit/{id}")]
        public async Task<PropertyVM> GetPropertyByIdToEdit(int id)
        {
            return await _clientPropertyService.GetPropertyByIdToEdit(id);
        }

        [HttpGet]
        [Route("GetRelatedProperties/{id}")]
        public Task<List<PropertyVM>> GetRelatedProperties(int id)
        {
            return _clientPropertyService.GetRelatedProperties(id);
        }

        [HttpGet]
        [Route("GetFeaturedProperties")]
        public Task<List<PropertyVM>> GetFeaturedProperties()
        {
            var property = _clientPropertyService.GetFeaturedProperties();
            _clientPropertyService.AddViewCount(property.Id);
            return property;
        }

        [HttpGet]
        [Route("GetPropertyOwner/{propertyId}")]
        public Task<ClientUserViewModel> GetPropertyOwner(int propertyId)
        {
            return _clientPropertyService.GetPropertyOwner(propertyId);
        }
    }
}