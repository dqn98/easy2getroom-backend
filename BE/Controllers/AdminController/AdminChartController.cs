using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminChartController : ControllerBase
    {
        private IChartService _chartService;

        public AdminChartController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetPropertyChartData/{year}")]
        public IActionResult GetPropertyChartData(string year)
        {
            var data = _chartService.GetPropertyChart(year);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }     
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetTotalPropertiesChartData/{year}")]
        public IActionResult GetTotalPropertiesChartData(string year)
        {
            if (year == "None")
            {
                year = null;
            }
            var data = _chartService.GetTotalPropertyChart(year);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetPropertyCategoryChartData/{year}")]
        public IActionResult GetPropertyCategoryChartData(string year)
        {
            if (year == "None")
            {
                year = null;
            }
            var data = _chartService.GetPropertyCategoryChart(year);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetTotalRentalTypeChartData/{year}")]
        public IActionResult GetTotalRentalTypeChartData(string year)
        {
            if(year == "None")
            {
                year = null;
            }
            var data = _chartService.GetRentalTypeChart(year);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }
    }
}