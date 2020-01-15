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
        private static readonly IDictionary<string, string> rooms = new Dictionary<string, string>();

        public override async Task<JoinRoomResponse> JoinRoom(JoinRoomRequest request, ServerCallContext context)
        {
            string connectionId = context.GetHttpContext().Connection.Id;

            rooms[connectionId] = request.RoomId;

            return new JoinRoomResponse();
        }

        public override async Task SendMessage(
            IAsyncStreamReader<ChatMessage> requestStream, 
            IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        {
            await foreach(var request in requestStream.ReadAllAsync(context.CancellationToken))
            {
                await responseStream.WriteAsync(new ChatMessage { Content = request.Content });
            }

            //while (await requestStream.MoveNext(context.CancellationToken))
            //{

            //}
        }


    }
}
