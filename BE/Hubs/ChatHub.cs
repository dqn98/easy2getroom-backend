using BE.Application.Interfaces;
using BE.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private IMessageService _messageService;
        private UserManager<User> _userManager;

        public ChatHub(IMessageService messageService, UserManager<User> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }

        public async Task SendMessage(Message message)
        {
            //Receive r
            List<string> RecipientConnectionIds = _connections.GetConnections(message.RecipientId.ToString()).ToList<string>();
            if (RecipientConnectionIds.Count() > 0)
            {
                //Save-Receive-Message
                try
                {
                    await Clients.Clients(RecipientConnectionIds).SendAsync("ReceiveMessage", message);
                }
                catch (Exception) { }
            }
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                var userId = httpContext.Request.Query["user"].ToString();
                var connId = Context.ConnectionId.ToString();
                _connections.Add(userId, connId);

                //Update Client
                await Clients.All.SendAsync("UpdateUserList");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                //Remove Logged User
                var userId = httpContext.Request.Query["user"];
                _connections.Remove(userId, Context.ConnectionId);

                //Update Client
                await Clients.All.SendAsync("UpdateUserList");
            }
        }
    }
}