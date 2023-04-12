using BE.Application.Interfaces;
using BE.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private IFunctionService _functionService;

        public FunctionController(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetFunctions")]
        public async Task<List<Function>> GetFunctions()
        {
            var data = await _functionService.GetAll();
            return data;
        }
    }
}