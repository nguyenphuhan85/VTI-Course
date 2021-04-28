using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Models.Notification;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CleanArchitecture.Application.HubSignalR
{
    public class ProductEventsHub : Hub
    {
        internal void SendAsync(string message, PushNotification @event, CancellationToken cancellationToken)
        {
            Console.WriteLine($"message={message}, @event={JsonConvert.SerializeObject(@event)}");
        }
    }
}
