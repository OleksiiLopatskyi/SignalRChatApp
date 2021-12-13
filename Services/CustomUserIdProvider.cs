using Microsoft.AspNetCore.SignalR;
using SignalRChatApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Services
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            var user = connection.User.Identity.Name;
            return user;
        }
    }
}
