using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using SignalRChatApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChatApp.Controllers
{
    public class ChatController : Controller
    {
        private ChatContext _db;
        private static Chat _chat;
        private static User _userTo, _userFrom;
        IHubContext<ChatHub> _hubContext;

        public ChatController(ChatContext context,IHubContext<ChatHub>hub)
        {
            _db = context;
            _hubContext = hub;
        }
        public IActionResult Index()
        {
            return View(_db.Users.Where(i=>i.Username!=User.Identity.Name));
        }
        [Route("[controller]/[action]/{name}")]
        public async Task<IActionResult> ChatRoom(string name)
        {
           
            var UserFrom = await _db.Users.FirstOrDefaultAsync(i=>i.Username== User.Identity.Name);
            var UserTo = await _db.Users.FirstOrDefaultAsync(i => i.Username == name);
            var Chat = await _db.Chats.Include(i=>i.Messages).Include(i=>i.UserFrom).Include(i=>i.UserTo).FirstOrDefaultAsync(i => i.UserFrom == UserFrom && i.UserTo == UserTo
                                                           || i.UserFrom == UserTo && i.UserTo == UserFrom);
            _userFrom = UserFrom;
            _userTo = UserTo;

            if (Chat == null)
                 {
                    Chat createdChat = new Chat()
                    {
                        UserFrom = UserFrom,
                        UserTo = UserTo,
                    };
                    Chat = createdChat;
                    await _db.Chats.AddAsync(Chat);
                    await _db.SaveChangesAsync();
                 }
            _chat = Chat;
            return View(Chat);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            Message createdMessage = new Message {
                UserFrom = _userFrom.Username,
                UserTo = _userTo.Username,
                Date = DateTime.Now.ToString("hh:mm"),
                Text=message
            };
            await _hubContext.Clients.Users(new string[] { _userTo.Username, _userFrom.Username })
                                     .SendAsync("Receive",createdMessage,_userFrom.Username,_userTo.Username);

            /*_chat.Messages.Add(createdMesage);
            _db.Chats.Update(_chat);
            _db.SaveChanges();*/
            return RedirectToAction("ChatRoom",new {name=createdMessage.UserTo});
        }
    }
}
