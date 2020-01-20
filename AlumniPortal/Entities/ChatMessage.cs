using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Models;

namespace AlumniPortal.Entities
{
    public class ChatMessage
    {
        public int ChatMessageId { get; set; }
        public int ConversationId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public bool IsRead { get; set; }
    }
}