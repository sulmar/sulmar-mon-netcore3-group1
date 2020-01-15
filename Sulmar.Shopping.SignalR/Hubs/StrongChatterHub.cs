using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.SignalR;
using Sulmar.Shopping.Domain.Models;
using Sulmar.Shopping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sulmar.Shopping.SignalR.Hubs
{
    public class StrongChatterHub : Hub<IChatter>
    {
        private static readonly IDictionary<string, string> rooms= new Dictionary<string, string>();

        public StrongChatterHub()
        {
        }

        public override async Task OnConnectedAsync()
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "MON");

            base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
          //  this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, rooms[this.Context.ConnectionId]);
           // rooms.Remove(this.Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);

        }

        public async Task JoinRoom(string roomId)
        {
            if (rooms.Values.Contains(roomId))
            {
                throw new ApplicationException();
            }

            rooms[this.Context.ConnectionId] = roomId;

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
