using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AlumniPortal.DbContext;
using AlumniPortal.Entities;
using AlumniPortal.Models;

namespace AlumniPortal.Repositories
{
    public class MyConnectRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Conversation> GetConversations(string id)
        {
            var result =
                db.Conversations
                .Include(x => x.UserA)
                .Include(x => x.UserB)
                .Include(c => c.Messages)
                .Where(c => c.UserA.Id == id || c.UserB.Id == id).ToList();
            return result;
        }

        public Conversation GetConversation(string userId, int convId)
        {
            return
                db.Conversations
                    .Include(c => c.Messages)
                    .Include(c => c.UserA)
                    .Include(c => c.UserB)
                    .FirstOrDefault(c => c.ConversationId == convId && c.UserA.Id == userId || c.UserB.Id == userId);
        }

        public async Task AddMessage(int convId, string senderId, string message)
        {
            var conv = db.Conversations.First(c => c.ConversationId == convId);
            conv.LastModified = DateTime.Now;
            conv.Messages.Add(new ChatMessage
            {
                Body = message,
                SenderId = senderId,
                CreatedDate = DateTime.Now
            });
            await db.SaveChangesAsync();
        }

        public ConversationStatus GetConnectionStatus(string userAId, string userBId)
        {
            var result = db.Conversations.FirstOrDefault(c => c.UserA.Id == userAId && c.UserB.Id == userBId);

            if (result == null) return ConversationStatus.None;

            switch (result.UserBAccepted)
            {
                case null:
                    return ConversationStatus.Awaiting;
                case true:
                    return ConversationStatus.Connected;
                case false:
                    return ConversationStatus.Declined;
                default:
                    return ConversationStatus.None;
            }
        }

        public bool InitiateConversation(string userAId, string userBId)
        {
            try
            {
                //creating a connection is the same as sending a friend request
                var conversation = new Conversation
                {
                    UserA = db.Users.Find(userAId),
                    UserB = db.Users.Find(userBId),
                    UserBAccepted = null,
                    LastModified = DateTime.Now
                };

                db.Conversations.Add(conversation);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateConversationStatus(int conversationId, string userId, bool isAccepted)
        {
            var conv = db.Conversations.Include(u => u.UserA).FirstOrDefault(c => c.ConversationId == conversationId);
            //varify that the user accepting is the right one
            if (conv != null && conv.UserA.Id == userId)
            {
                conv.UserBAccepted = isAccepted;
                conv.LastModified = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}