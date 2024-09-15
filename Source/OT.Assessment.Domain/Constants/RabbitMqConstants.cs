using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Core.Domain.Constants
{
    public class RabbitMqConstants
    {
        public const string RabbitMqRootUri = "rabbitmq://localhost";
        public const string RabbitMqCreateCasinoWagerQueueUri = "rabbitmq://localhost/create-casinowager";
        public const string UserName = "guest";
        public const string Password = "guest";
        //public const string NotificationServiceQueue = "notification.service";
    }
}
