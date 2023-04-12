using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IClientUserService _clientUserService;
        private IClientPropertyService _clientPropertyService;

        public UserController(IClientUserService clientUserService, IClientPropertyService clientPropertyService)
        {
            _clientUserService = clientUserService;
            _clientPropertyService = clientPropertyService;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<List<ClientUserViewModel>> GetUsers()
        {
            return await _clientUserService.GetUsers();
        }

        [HttpGet]
        [Route("GetUser/{username}")]
        public async Task<ClientUserViewModel> GetUser(string username)
        {
            return await _clientUserService.GetUser(username);
        }

        [HttpGet]
        [Route("GetPropertiesByUsername/{username}")]
        public async Task<List<PropertyVM>> GetPropertiesByUsername(string username)
        {
            return await _clientPropertyService.GetPropertiesByUsername(username);
        }
    }
}