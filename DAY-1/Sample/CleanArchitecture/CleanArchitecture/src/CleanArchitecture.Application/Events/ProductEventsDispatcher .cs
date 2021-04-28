using CleanArchitecture.Application.HubSignalR;
using CleanArchitecture.Application.Models.Notification;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Events
{
   public class ProductEventsDispatcher
    {
        private readonly IHubContext<ProductEventsHub>
           _hubContext;
        public ProductEventsDispatcher(
           IHubContext<ProductEventsHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task Handle(PushNotification @event,
           CancellationToken cancellationToken)
        {
            _hubContext.Clients.All.SendAsync("Product",
               @event, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
