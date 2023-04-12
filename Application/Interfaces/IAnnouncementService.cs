using BE.Application.ViewModels.Shared.Announcement;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IAnnouncementService : IDisposable
    {
        Task<List<AnnouncementViewModel>> GetReceivedAnnouncements(Guid userId);

        Task<AnnouncementViewModel> SaveAnnouncement(AddAnnouncementViewModel viewModel);

        Task<bool> UpdateAnnouncementStatus(UpdateAnnouncementStatusViewModel viewModel);

        AnnouncementViewModel MappingData(Announcement announcement);

        Task<bool> RemoveAnnouncement(int id);

    }
}