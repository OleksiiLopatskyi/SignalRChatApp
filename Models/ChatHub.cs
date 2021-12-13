using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Models
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string message,string user)
        {
            var userName = Context.User.Identity.Name;
            if (Context.UserIdentifier != user) // если получатель и текущий пользователь не совпадают
                await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, userName);
            await Clients.User(user).SendAsync("Receive", message, userName);       
        }
        public string GetConnectionId() => Context.ConnectionId;
    }
}
