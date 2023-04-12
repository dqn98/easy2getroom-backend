using BE.Application.Interfaces;
using BE.Application.ViewModels.Client;
using BE.Application.ViewModels.Shared.Message;
using BE.Data.Entities;
using BE.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private IHubContext<ChatHub> _chatHubContext;
        private IMessageService _messageService;

        public MessageController(IHubContext<ChatHub> chatHubContext, IMessageService messageService)
        {
            _chatHubContext = chatHubContext;
            _messageService = messageService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("GetMessages")]
        public Task<List<Message>> GetMessages(GetMessageViewModel viewModel)
        {
            return _messageService.GetMessages(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        [Route("SendMessage")]
        public Task<Message> SendMessage(MessageViewModel viewModel) 
        {
            List<string> RecipientConnectionIds = _connections.GetConnections(viewModel.RecipientId.ToString()).ToList<string>();
            viewModel.IsPrivate = true;
            viewModel.Connectionid = String.Join(",", RecipientConnectionIds);
            return _messageService.SaveMessage(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Client")]
        [Route("GetUserMessages/{userId}")]
        public Task<List<ClientUserViewModel>> GetUserMessages(Guid userId)
        {
            return _messageService.GetUserMessages(userId);
        }
    }
}