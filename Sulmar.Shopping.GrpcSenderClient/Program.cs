using Grpc.Net.Client;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sulmar.Shopping.GrpcSenderClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Hello gRPC Sender!");

            const string url = "https://localhost:5001";

            var channel = GrpcChannel.ForAddress(url);
            var client = new ChatterService.ChatterManager.ChatterManagerClient(channel);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                ChatterService.ChatMessage chatMessage = new ChatterService.ChatMessage { Content = $"Temp {i}" };

                 await client.SendMessage().RequestStream.WriteAsync(chatMessage);
            }

            stopwatch.Stop();
            Console.WriteLine($"elapsed time: {stopwatch.Elapsed}");

            Console.ReadKey();

            Console.ResetColor();



        }
    }
}
