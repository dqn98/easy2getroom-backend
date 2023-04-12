using BE.Application.Interfaces;
using BE.Application.ViewModels.Shared.Announcement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("SaveAnnouncement")]
        public Task<AnnouncementViewModel> SaveAnnouncement(AddAnnouncementViewModel viewModel)
        {
            return _announcementService.SaveAnnouncement(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("GetReceivedAnnouncements")]
        public Task<List<AnnouncementViewModel>> GetReceivedAnnouncements(GetAnnouncementViewModel viewModel)
        {
            return _announcementService.GetReceivedAnnouncements(viewModel.UserId);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("UpdateAnnouncementStatus")]
        public Task<bool> UpdateAnnouncementStatus(UpdateAnnouncementStatusViewModel viewModel)
        {
            return _announcementService.UpdateAnnouncementStatus(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Client")]
        [Route("RemoveAnnouncement/{id}")]
        public Task<bool> RemoveAnnouncement(int id)
        {
            return _announcementService.RemoveAnnouncement(id);
        }
    }
}