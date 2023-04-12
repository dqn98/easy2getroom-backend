using BE.Application.Interfaces;
using BE.Application.ViewModels.Admin.Logging;
using BE.Data.Entities;
using BE.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoggingController : ControllerBase
    {
        private IHubContext<LoggingHub> _loggingHubContext;
        private ILoggingService _loggingService;
        private ILogTypeService _logTypeService;

        public AdminLoggingController(IHubContext<LoggingHub> loggingHubContext, ILoggingService loggingService, ILogTypeService logTypeService)
        {
            _loggingHubContext = loggingHubContext;
            _loggingService = loggingService;
            _logTypeService = logTypeService;
        }

        [HttpGet]
        [Route("GetConsoleMessage")]
        public IActionResult GetConsoleMessage()
        {
            _loggingHubContext.Clients.All.SendAsync("Send", "Hello from the server");
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("GetLogs")]
        public async Task<List<LogViewModel>> GetLogs(GetLogViewModel viewModel)
        {
            return await _loggingService.GetLogs(viewModel);
        }

        [HttpPost]
        [Route("AddLog")]
        public async Task<LogViewModel> AddLog(AddLogViewModel viewModel)
        {
            var log =  await _loggingService.AddLog(viewModel);
            return log;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetLogTypes")]
        public async Task<List<LogType>> GetLogTypes()
        {
            return await _logTypeService.GetLogTypes();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AddLogType")]
        public async Task<IActionResult> AddLogType(AddLogTypeViewModel viewModel)
        {
            var result = await _logTypeService.AddLogType(viewModel);
            if (result)
            {
                return Ok(true);
            }
            return BadRequest();
        }
    }
}