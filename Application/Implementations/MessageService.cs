using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Shared.Message;
using BE.Data.Entities;
using BE.Data.IRepositories;
using BE.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Application.Implementations
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<User> _userManager;

        public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<Message>> GetMessages(GetMessageViewModel viewModel)
        {
            var sender = await _userManager.FindByIdAsync(viewModel.SenderId.ToString());
            var recipient = await _userManager.FindByIdAsync(viewModel.RecipientId.ToString());

            var senderId = sender.Id;
            var recipientId = recipient.Id;

            var messages = _messageRepository.FindAll(m => m.Sender, m => m.Recipient)
                .Where(m => (m.SenderId == senderId && m.RecipientId == recipientId) || (m.SenderId == recipientId && m.RecipientId == senderId))
                .ToListAsync();

            return await messages;
        }

        public async Task<Message> SaveMessage(MessageViewModel viewModel)
        {
            var message = new Message();
            message.Connectionid = viewModel.Connectionid;
            message.SenderId = viewModel.SenderId;
            message.RecipientId = viewModel.RecipientId;
            message.Content = viewModel.Content;
            message.Status = viewModel.Status;
            message.IsRead = viewModel.IsRead;
            message.IsGroup = viewModel.IsGroup;
            message.IsMultiple = viewModel.IsMultiple;
            message.IsPrivate = viewModel.IsPrivate;
            message.MessageSent = DateTime.Now;

            _messageRepository.Add(message);
            await _unitOfWork.CommitAllAsync();

            var messageFromDb = _messageRepository.FindAll(m => m.Sender, m => m.Recipient)
                .FirstOrDefault(m => m.Id == message.Id);

            return messageFromDb;
        }

        public async Task<ClientUserViewModel> MappingUserAsync(User user)
        {
            var role = await _userManager.GetRolesAsync(user);

            var vm = new ClientUserViewModel();
            vm.Id = user.Id;
            vm.Username = user.UserName;
            vm.FullName = user.FullName;
            vm.Desc = "Address: " + user.Address + ", Date registed: " + user.DateCreated.ToString();
            vm.Organization = "Easy2GetRoom";
            vm.Email = user.Email;
            vm.Phone = user.PhoneNumber;

            vm.Social = new Social();
            vm.Social.Facebook = user.FacebookUrl;
            vm.Social.Twitter = user.TwitterUrl;
            vm.Social.Website = user.WebsiteUrl;
            vm.Social.Instagram = "";
            vm.Social.Linkedin = "";
            vm.RoleName = role.FirstOrDefault();
            vm.Image = user.Avatar;

            return vm;
        }

        public async Task<List<ClientUserViewModel>> GetUserMessages(Guid userId)
        {
            var messages = await _messageRepository.FindAll().Where(m => m.RecipientId == userId || m.SenderId == userId).ToListAsync();

            var users = await _userManager.Users.ToListAsync();

            var result = new List<ClientUserViewModel>();

            foreach (Message message in messages)
            {
                foreach (User user in users)
                {
                    if (user.Id == message.RecipientId || user.Id == message.SenderId)
                    {
                        var vm = new ClientUserViewModel();
                        vm = await MappingUserAsync(user);

                        result.Add(vm);
                    }
                }
            }
            result = result.GroupBy(o => o.Id).Select(o => o.FirstOrDefault()).ToList();
            return result;
        }
    }
}