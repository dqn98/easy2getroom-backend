using BE.Application.Interfaces;
using BE.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        [Route("GetFeatures")]
        public async Task<List<Feature>> GetFeatures()
        {
            return await _featureService.GetFeatures();
        }
    }
}