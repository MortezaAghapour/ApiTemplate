using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTemplate.Core.DataTransforObjects.RabitMq
{
    public class PublishMessageModel
    {
        public string QueueName { get; set; }
        public string RouteKey { get; set; }
        public string Message { get; set; }
    }
}
