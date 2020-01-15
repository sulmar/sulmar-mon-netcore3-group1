using Microsoft.AspNetCore.SignalR;
using Sulmar.Shopping.Domain.Models;
using Sulmar.Shopping.Domain.Services;
using System.Threading.Tasks;

namespace Sulmar.Shopping.SignalR.Hubs
{
    public class StrongChatterHub : Hub<IChatter>
    {
        public override async Task OnConnectedAsync()
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "MON");

            base.OnConnectedAsync();
        }

        public async Task JoinRoom(string roomId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, roomId);
        }

        public async Task SendMessage(ChatMessage message, string roomId)
        {
            //await this.Clients.All.HaveGotMessage(message);

            //await this.Clients.Others.HaveGotMessage(message);

            await this.Clients.Group(roomId).HaveGotMessage(message);


        }
    }
}
