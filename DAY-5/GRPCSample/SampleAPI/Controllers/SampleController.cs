using Grpc.Core;
using GRPCClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly Payment.PaymentClient _paymentClient;

        public SampleController(Payment.PaymentClient paymentClient)
        { _paymentClient = paymentClient; }
        // GET: api/<SampleController>
        [HttpGet]
        public IActionResult Get()
        {
            var reply = _paymentClient.GetPayment(new GetPaymentRequestModel()
            {
                OrderId = Guid.NewGuid().ToString()
            });
            return Ok(reply);
        }

        // Server Streaming type
        [HttpGet("server-stream")]
        public async Task<IActionResult> GetServerStream(int id)
        {
            string result = "";
            // Server Streaming type
            using var statusReplies = _paymentClient.GetPaymentStatus(new GetPaymentStatusRequestModel { OrderId = Guid.NewGuid().ToString() });
            while (await statusReplies.ResponseStream.MoveNext())
            {
                var status = statusReplies.ResponseStream.Current.Status;
                result += status + ":";
            }
            return Ok(result);
        }

        // Client Streaming type
        [HttpGet("client-stream")]
        public async Task<IActionResult> GetClientStream(int id)
        {
            CashoutResponseModel result =new CashoutResponseModel();
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

            using (var call = _paymentClient.CashOut())
            {
                foreach (var requestModel in cashOutRequestModels)
                {
                    await call.RequestStream.WriteAsync(requestModel);
                }
                await call.RequestStream.CompleteAsync();

                result = await call.ResponseAsync;
            }

            return Ok(result.TotalAmount);
        }


        // Bi-directional streaming
        [HttpGet("bidirectional-stream")]
        public async Task<IActionResult> GetBidirectionalStream(int id)
        {
            string result = "";
            var requests = new List<SendingMessage>();

            for (int i = 1; i < 10; i++)
            {
                requests.Add(new SendingMessage { Number = i });
            }

            using (var call = _paymentClient.CustomerCommunication())
            {
                var responseReaderTask = Task.Run(async () =>
                {
                    while (await call.ResponseStream.MoveNext())
                    {
                        var note = call.ResponseStream.Current;
                        result += note + ":";
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
            return Ok(result);
        }

        // POST api/<SampleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SampleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SampleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
