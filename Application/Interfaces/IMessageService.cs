using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Shared.Message;
using BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Application.Interfaces
{
    public interface IMessageService : IDisposable
    {
        Task<Message> SaveMessage(MessageViewModel viewModel);

        Task<List<Message>> GetMessages(GetMessageViewModel viewModel);

        Task<ClientUserViewModel> MappingUserAsync(User user);

        Task<List<ClientUserViewModel>> GetUserMessages(Guid userId);
    }
}