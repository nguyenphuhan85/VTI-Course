using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GRPCClient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var paymentClient = new Payment.PaymentClient(channel);

            Console.WriteLine("Welcome to GRPC Client");
            // Unary type
            var reply = paymentClient.GetPayment(new GetPaymentRequestModel { OrderId = Guid.NewGuid().ToString() });
            Console.WriteLine(reply.Orderdate);

            // Server Streaming type
            using var statusReplies = paymentClient.GetPaymentStatus(new GetPaymentStatusRequestModel { OrderId = Guid.NewGuid().ToString() });
            while (await statusReplies.ResponseStream.MoveNext())
            {
                var status = statusReplies.ResponseStream.Current.Status;
                Console.WriteLine(status);
            }

            // Client Streaming type
            var cashOutRequestModels = new List<CashOutRequestModel>();
            cashOutRequestModels.Add(new CashOutRequestModel
            {
                ProductId = Guid.NewGuid().ToString(),
                Quantity = 2,
                Amount = 1000
            });

            cashOutRequestModels.Add(new CashOutRequestModel
            {
                ProductId = Guid.NewGuid().ToString(),
                Quantity = 5,
                Amount = 400
            });

            cashOutRequestModels.Add(new CashOutRequestModel
            {
                ProductId = Guid.NewGuid().ToString(),
                Quantity = 4,
                Amount = 2000.50
            });

            using (var call = paymentClient.CashOut())
            {
                foreach (var requestModel in cashOutRequestModels)
                {
                    await call.RequestStream.WriteAsync(requestModel);
                }
                await call.RequestStream.CompleteAsync();

                var result = await call.ResponseAsync;
                Console.WriteLine(result.TotalAmount);
            }


            // Bi-directional streaming
            var requests = new List<SendingMessage>();

            for (int i = 1; i < 10; i++)
            {
                requests.Add(new SendingMessage { Number = i });
            }

            using (var call = paymentClient.CustomerCommunication())
            {
                var responseReaderTask = Task.Run(async () =>
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        var note = call.ResponseStream.Current;
                        Console.WriteLine("Received " + note);
                    }
                });

                foreach (var request in requests)
                {
                    await call.RequestStream.WriteAsync(request);
                    Thread.Sleep(5000);
                }
                await call.RequestStream.CompleteAsync();
                await responseReaderTask;
            }

            Console.ReadLine();
        }
    }
}
