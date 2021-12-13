using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public User UserTo { get; set; }
        public User UserFrom { get; set; }
        public List<Message> Messages { get; set; }
        public Chat()
        {
            Messages = new List<Message>();
        }
    }
}
