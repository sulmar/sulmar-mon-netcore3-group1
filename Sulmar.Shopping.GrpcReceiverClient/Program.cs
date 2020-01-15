using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Sulmar.Shopping.GrpcReceiverClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Hello gRPC Receiver!");

            const string url = "https://localhost:5001";

            var channel = GrpcChannel.ForAddress(url);
            var client = new ChatterService.ChatterManager.ChatterManagerClient(channel);

            var stream = client.SendMessage().ResponseStream.ReadAllAsync();

            await foreach(var message in stream)
            {
                Console.WriteLine(message.Content);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();

        }
    }
}
