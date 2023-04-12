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
    public class AdminCityController : ControllerBase
    {
        private ICityService _cityService;

        public AdminCityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetAllCities")]
        public async Task<List<City>> GetAllCities()
        {
            return await _cityService.GetAll();
        }
    }
}