using BE.Application.ViewModels.Admin.Logging;
using BE.Data.Entities;
using BE.Ultilities.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Hubs
{
    public class LoggingHub : Hub
    {
        private UserManager<User> _userManager;
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public LoggingHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendLog(LogViewModel log)
        {
            var users = await _userManager.Users.ToListAsync();
            List<string> RecipientConnectionIds = new List<string>();
            foreach (User u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                var role = roles.FirstOrDefault();
                if (role.Equals(CommonConstants.AdminRole))
                {
                    var conn = _connections     .GetConnections(u.Id.ToString()).ToList<string>().FirstOrDefault();
                    if (conn != null)
                    {
                        RecipientConnectionIds.Add(conn);
                    }
                }
            }
            if (RecipientConnectionIds.Count() > 0)
            {
                //Save-Receive-Message
                try
                {
                    await Clients.Clients(RecipientConnectionIds).SendAsync("ReceiveNewLog", log);
                }
                catch (Exception ex) {
                    var e = ex.ToString();
                }
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