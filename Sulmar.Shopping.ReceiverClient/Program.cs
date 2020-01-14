using Microsoft.AspNetCore.SignalR.Client;
using Sulmar.Shopping.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Sulmar.Shopping.ReceiverClient
{
    class Program
    {
        // dotnet add package Microsoft.AspNetCore.SignalR.Client
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Hello Signal-R Receiver!");

            const string url = "http://localhost:5000/signalr/chatter";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            await connection.SendAsync("JoinRoom", "Klon");

            connection.On<ChatMessage>("HaveGotMessage", message => Console.WriteLine($"Received {message.Content}"));

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }
    }
}
