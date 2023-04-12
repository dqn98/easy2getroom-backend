using BE.Application.Interfaces;
using BE.Application.ViewModels.Shared.Announcement;
using BE.Data.Entities;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class AnnouncementService : IAnnouncementService
    {
        private IAnnouncementRepository _announcementRepository;
        private IUnitOfWork _unitOfWork;

        public AnnouncementService(IAnnouncementRepository announcementRepository, IUnitOfWork unitOfWork)
        {
            _announcementRepository = announcementRepository;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<AnnouncementViewModel>> GetReceivedAnnouncements(Guid userId)
        {
            var announcements = await _announcementRepository
                .FindAll(a => a.AnnouncementType)
                .Where(a => a.ReceiverId == userId)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();

            var results = new List<AnnouncementViewModel>();
            if (announcements.Count() != 0)
            {
                foreach (Announcement a in announcements)
                {
                    var vm = MappingData(a);
                    results.Add(vm);
                }
            }
            return results;
        }

        public AnnouncementViewModel MappingData(Announcement announcement)
        {
            var vm = new AnnouncementViewModel();

            vm.Id = announcement.Id;
            vm.Content = announcement.Content;
            vm.Type = announcement.AnnouncementType.Name;
            vm.Icon = announcement.AnnouncementType.Icon;
            vm.IsRead = announcement.IsRead;
            vm.Date = announcement.DateCreated;
            vm.SenderId = announcement.SenderId;
            vm.ReceiverId = announcement.ReceiverId;

            return vm;
        }

        public async Task<bool> RemoveAnnouncement(int id)
        {
            var announcement = await _announcementRepository.FindByIdAsync(id);
            _announcementRepository.Remove(announcement);
            return await _unitOfWork.CommitAllAsync();
        }

        public async Task<AnnouncementViewModel> SaveAnnouncement(AddAnnouncementViewModel viewModel)
        {
            var announcement = new Announcement();
            announcement.Content = viewModel.Content;
            announcement.SenderId = viewModel.SenderId;
            announcement.ReceiverId = viewModel.ReceiverId;
            announcement.AnnouncementTypeId = viewModel.AnnouncementTypeId;

            _announcementRepository.Add(announcement);
            var isSaveSuccess = await _unitOfWork.CommitAllAsync();
            if (isSaveSuccess)
            {
                var announcementFromDb = await _announcementRepository.FindAll(a => a.AnnouncementType).Where(a => a.Id == announcement.Id).FirstOrDefaultAsync();
                return MappingData(announcementFromDb);
            }
            return null;
        }

        public async Task<bool> UpdateAnnouncementStatus(UpdateAnnouncementStatusViewModel viewModel)
        {
            var announcement = await _announcementRepository.FindByIdAsync(viewModel.Id);
            announcement.IsRead = viewModel.IsRead;

            _announcementRepository.Update(announcement);

            return await _unitOfWork.CommitAllAsync();
        }
    }
}