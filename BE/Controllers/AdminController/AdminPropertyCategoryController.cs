using BE.Application.Interfaces;
using BE.Data.Entities;
using BE.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPropertyCategoryController : ControllerBase
    {
        private IPropertyCategoryService _propertyCategoryService;
        private IUnitOfWork _unitOfWork;

        public AdminPropertyCategoryController(IPropertyCategoryService propertyCategoryService, IUnitOfWork unitOfWork)
        {
            _propertyCategoryService = propertyCategoryService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllPropertyCategories")]
        public async Task<List<PropertyCategory>> GetAllPropertyCategories()
        {
            return await _propertyCategoryService.GetAll();
        }
    }
}