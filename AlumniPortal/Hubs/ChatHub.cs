using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using AlumniPortal.Repositories;

namespace AlumniPortal.Hubs
{
    public class ChatHub : Hub
    {
        private MyConnectRepository repo = new MyConnectRepository();

        public async Task SendMessage(int convId, string to, string from, string message)
        {
            var date = DateTime.Now;
            Clients.Group(to).addChatMessage(message, date);
            await repo.AddMessage(convId, from, message);
        }

        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();
            Groups.Add(Context.ConnectionId, userId);

            return base.OnConnected();
        }
    }
}