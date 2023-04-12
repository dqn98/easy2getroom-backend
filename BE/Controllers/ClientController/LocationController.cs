using BE.Application.Interfaces;
using BE.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ICityService _cityService;
        private IDistrictService _districtService;
        private IWardsService _wardsService;

        public LocationController(ICityService cityService, IDistrictService districtService, IWardsService wardsService)
        {
            _cityService = cityService;
            _districtService = districtService;
            _wardsService = wardsService;
        }

        [HttpGet]
        [Route("GetCities")]
        public Task<List<City>> GetCities()
        {
            return _cityService.GetAll();
        }

        [HttpGet]
        [Route("GetDistricts/{cityId}")]
        public Task<List<District>> GetDistricts(int cityId)
        {
            return _districtService.GetDistrictbyCityId(cityId);
        }

        [HttpGet]
        [Route("GetWards/{districtId}")]
        public Task<List<Wards>> GetWards(int districtId)
        {
            return _wardsService.GetWardsByDistrictId(districtId);
        }
    }
}