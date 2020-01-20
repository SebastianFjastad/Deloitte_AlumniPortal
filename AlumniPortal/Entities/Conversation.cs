using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlumniPortal.Models;

namespace AlumniPortal.Entities
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public ApplicationUser UserA { get; set; } 
        public ApplicationUser UserB { get; set; }
        public bool? UserBAccepted { get; set; }
        public DateTime LastModified { get; set; } 

        public virtual ICollection<ChatMessage> Messages { get; set; } 
    }
}