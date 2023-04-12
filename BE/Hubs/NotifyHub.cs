using BE.Application.ViewModels.Shared.Announcement;
using BE.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Hubs
{
    public class NotifyHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        private readonly UserManager<User> _userManager;

        public NotifyHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendAnnouncement(AnnouncementViewModel announcement)
        {
            //Receive r
            List<string> RecipientConnectionIds = _connections.GetConnections(announcement.ReceiverId.ToString()).ToList<string>();
            if (RecipientConnectionIds.Count() > 0)
            {
                //Save-Receive-Message
                try
                {
                    await Clients.Clients(RecipientConnectionIds).SendAsync("ReceiveAnnouncement", announcement);
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
                await Clients.All.SendAsync("CreateConnection");
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
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     