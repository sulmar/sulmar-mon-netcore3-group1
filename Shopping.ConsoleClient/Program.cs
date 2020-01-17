using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Primitives;
using Shopping.PaymentService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Shopping.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            float x = 10;
            float y = 0;

            float result = x / y;

            checked
            {
                byte z = 255;

                z++;
                z++;
            }

      //      await StreamClientTest();

            //FileSystemWatcherTest();

          //  await gRPCClientTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();


        }

        private static void FileSystemWatcherTest()
        {
            FileSystemWatcher watcher = new FileSystemWatcher("readme.txt");
            watcher.NotifyFilter = NotifyFilters.LastWrite;



        }

        private static async Task gRPCClientTest()
        {
            Console.WriteLine("Hello gRPC Client!");

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new PaymentManager.PaymentManagerClient(channel);

            var request = new MakePaymentRequest { ProductId = "Notebook", Quantity = 5, Address = "Plac Inwalidow" };

            var response = await client.MakePaymentAsync(request);

            Console.WriteLine($"response: OrderId = {response.OrderId}");
        }

        private static async Task StreamClientTest()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new PaymentManager.PaymentManagerClient(channel);

            var statusResponse = client.GetPaymentStatus(new GetPaymentStatusRequest { OrderId = "abc" });

            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //CancellationToken token = cancellationTokenSource.Token;

            //cancellationTokenSource.Cancel();
           

            var stream = statusResponse.ResponseStream.ReadAllAsync();

            Random random = new Random();

            await foreach(var status in stream)
            {
                await Task.Delay(TimeSpan.FromSeconds(random.NextDouble()));

                Console.WriteLine($"{status.Status}");
            }

        }
    }
}
