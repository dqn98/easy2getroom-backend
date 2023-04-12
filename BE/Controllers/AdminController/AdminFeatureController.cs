using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Feature;
using BE.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminFeatureController : ControllerBase
    {
        private IFeatureService _featureService;

        public AdminFeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("GetFeatures")]
        public async Task<List<Feature>> GetFeatures(FeatureViewModel viewModel)
        {
             return await _featureService.GetFeatures(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetFeature/{id}")]
        public async Task<Feature> GetFeature(int id)
        {
            return await _featureService.GetFeature(id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AddFeature")]
        public async Task<IActionResult> AddFeature(FeatureViewModel viewModel)
        {
            var result = await _featureService.Add(viewModel);
            if(result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var result = await _featureService.Delete(id);
            if(result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}