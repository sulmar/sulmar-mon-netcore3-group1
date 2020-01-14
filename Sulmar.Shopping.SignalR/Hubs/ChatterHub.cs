using Microsoft.AspNetCore.SignalR;
using Sulmar.Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sulmar.Shopping.SignalR.Hubs
{
    public class ChatterHub : Hub
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
            // await this.Clients.All.SendAsync("HaveGotMessage", message);

            //await this.Clients.Others.SendAsync("HaveGotMessage", message);

            await this.Clients.Group(roomId).SendAsync("HaveGotMessage", message);

           
        }

        public async Task Ping(string message = "Pong")
        {
            await this.Clients.Caller.SendAsync(message);
        }
    }
}
