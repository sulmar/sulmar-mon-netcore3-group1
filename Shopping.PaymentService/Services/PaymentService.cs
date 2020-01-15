using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Shopping.PaymentService.Services
{
    public class PaymentService : PaymentManager.PaymentManagerBase
    {
        private readonly ILogger<PaymentService> logger;

        public PaymentService(ILogger<PaymentService> logger)
        {
            this.logger = logger;
        }

        public override async Task<MakePaymentResponse> MakePayment(MakePaymentRequest request, ServerCallContext context)
        {
            logger.LogInformation($"Make payment for product {request.ProductId}");

            var orderId = Guid.NewGuid().ToString();

            return new MakePaymentResponse { OrderId = orderId };
        }


        public override async Task GetPaymentStatus(
            GetPaymentStatusRequest request, 
            IServerStreamWriter<GetPaymentStatusResponse> responseStream, 
            ServerCallContext context)
        {
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                responseStream.WriteAsync(new GetPaymentStatusResponse { Status = $"Created {i}" });
            //   await Task.Delay(TimeSpan.FromSeconds(random.NextDouble()));
            }

            await Task.Delay(TimeSpan.FromSeconds(5));

            await responseStream.WriteAsync(new GetPaymentStatusResponse { Status = "Validated" });

            await Task.Delay(TimeSpan.FromSeconds(10));

            await responseStream.WriteAsync(new GetPaymentStatusResponse { Status = "Sent" });

            await Task.Delay(TimeSpan.FromSeconds(4));



        }

    }
}
