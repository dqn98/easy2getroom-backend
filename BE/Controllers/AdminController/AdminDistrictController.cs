using BE.Application.Interfaces;
using BE.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;

        public AdminDistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllDistricts")]
        public async Task<List<District>> GetAllDistricts()
        {
            return await _districtService.GetAll();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllDistrictsByCityId/{cityId}")]
        public async Task<List<District>> GetAllDistrictsByCityId(int cityId)
        {
            return await _districtService.GetDistrictbyCityId(cityId);
        }
    }
}