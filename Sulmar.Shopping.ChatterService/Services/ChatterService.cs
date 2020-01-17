using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Sulmar.Shopping.ChatterService.Services
{
    public class ChatterService : ChatterManager.ChatterManagerBase
    {
        private static readonly IDictionary<IAsyncStreamWriter<ChatMessage>, string> rooms 
            = new Dictionary<IAsyncStreamWriter<ChatMessage>, string>();

        public override async Task<JoinRoomResponse> JoinRoom(JoinRoomRequest request, 
            ServerCallContext context)
        {
            string connectionId = context.GetHttpContext().Connection.Id;

         //   rooms[context.] = request.RoomId;

            return new JoinRoomResponse();
        }

        public override async Task SendMessage(
            IAsyncStreamReader<ChatMessage> requestStream, 
            IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        {
            string connectionId = context.GetHttpContext().Connection.Id;

            rooms[responseStream] = connectionId;

            await foreach(var request in requestStream.ReadAllAsync(context.CancellationToken))
            {
                foreach (var room in rooms)
                {
                    await room.Key.WriteAsync(new ChatMessage { Content = request.Content });
                }
                
            }

            //while (await requestStream.MoveNext(context.CancellationToken))
            //{

            //}
        }


    }
}
