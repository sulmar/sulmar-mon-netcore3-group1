using Microsoft.AspNetCore.SignalR.Client;
using Sulmar.Shopping.Domain.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sulmar.Shopping.SenderClient
{
    class Program
    {

        // <= C# 7.0
       // static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        // dotnet add package Microsoft.AspNetCore.SignalR.Client
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Hello Signal-R Sender!");

            const string url = "http://localhost:5000/signalr/chatter";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            //while(true)
            //{
            //    Console.Write("Type message: ");
            //    string content = Console.ReadLine();

            //    ChatMessage message = new ChatMessage { Content = content };

            //    await connection.SendAsync("SendMessage", message, "Klon");
            //}

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                ChatMessage message = new ChatMessage { Content = $"Temp {i}" };

                await connection.SendAsync("SendMessage", message, "Klon");
            }

            stopwatch.Stop();
            Console.WriteLine($"elapsed time: {stopwatch.Elapsed}");

            Console.ReadKey();

            Console.ResetColor();
        }
    }
}
