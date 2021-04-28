using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Models.Notification
{
    public class PushNotification:INotification
    {
         public string Message { get; set; }
        public DateTime LastDate { get; set; }
    }
}
