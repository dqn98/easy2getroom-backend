using BE.Application.Interfaces;
using BE.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyCategoryController : ControllerBase
    {
        private IPropertyCategoryService _propertyCategoryService;

        public PropertyCategoryController(IPropertyCategoryService propertyCategoryService)
        {
            _propertyCategoryService = propertyCategoryService;
        }

        [HttpGet]
        [Route("GetPropertyCategories")]
        public Task<List<PropertyCategory>> GetPropertyCategories()
        {
            return _propertyCategoryService.GetAll();
        }
    }
}