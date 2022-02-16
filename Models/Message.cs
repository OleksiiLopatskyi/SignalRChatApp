using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string UserTo { get; set; }
        public string UserFrom { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
    }
}
