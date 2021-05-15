using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceSample.Services
{
    public class PaymentService: Payment.PaymentBase
    {
        public override Task<MakePaymentReply> MakePayment(MakePaymentRequestModel requestModel, ServerCallContext context)
        {
            var result = new MakePaymentReply
            {
                ConfirmationMessage = $"Order is finished with Address is {requestModel.Address} at {requestModel.Orderdate.ToDateTime().ToString()}"
            };

            return Task.FromResult(result);
        }

        public override Task<GetPaymentReply> GetPayment(GetPaymentRequestModel requestModel, ServerCallContext context)
        {
            var result = new GetPaymentReply
            {
                ProductId = Guid.NewGuid().ToString(),
                Quantity = 2,
                Address = "Ha Noi - Viet Nam",
                Orderdate = Timestamp.FromDateTime(DateTime.UtcNow)
            };

            return Task.FromResult(result);
        }

        public override async Task GetPaymentStatus(GetPaymentStatusRequestModel request, IServerStreamWriter<GetPaymentStatusReply> response, ServerCallContext context)
        {
            await response.WriteAsync(new GetPaymentStatusReply { Status = "Ready" });
            await Task.Delay(5000);
            await response.WriteAsync(new GetPaymentStatusReply { Status = "Running" });
            await Task.Delay(5000);
            await response.WriteAsync(new GetPaymentStatusReply { Status = "Complete" });
        }

        public override async Task<CashoutResponseModel> CashOut(Grpc.Core.IAsyncStreamReader<CashOutRequestModel> requestStream, Grpc.Core.ServerCallContext context)
        {
            int count = 0;
            double totalAmount = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (await requestStream.MoveNext())
            {
                var product = requestStream.Current;
                count++;
                var tempAmount = product.Quantity * product.Amount;
                totalAmount += tempAmount;
            }

            stopwatch.Stop();

            return new CashoutResponseModel
            {
                TotalAmount = totalAmount
            };
        }

        public override async Task CustomerCommunication(Grpc.Core.IAsyncStreamReader<SendingMessage> requestStream, Grpc.Core.IServerStreamWriter<ReplyingMessage> responseStream, Grpc.Core.ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var number = requestStream.Current.Number;
                if (number % 2 == 0)
                {
                    await responseStream.WriteAsync(new ReplyingMessage { Reply = "This is an even number" });
                }
                else
                {
                    await responseStream.WriteAsync(new ReplyingMessage { Reply = "This is an odd number" });
                }
            }
        }
    }
}
