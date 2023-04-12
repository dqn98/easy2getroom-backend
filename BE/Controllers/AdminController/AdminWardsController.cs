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
    public class AdminWardsController : ControllerBase
    {
        private readonly IWardsService _wardsService;

        public AdminWardsController(IWardsService wardsService)
        {
            _wardsService = wardsService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllDistricts")]
        public async Task<List<Wards>> GetAllDistricts()
        {
            return await _wardsService.GetAll();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllWardsByDistrictId/{districtId}")]
        public async Task<List<Wards>> GetAllWardsByDistrictId(int districtId)
        {
            return await _wardsService.GetWardsByDistrictId(districtId);
        }
    }
}