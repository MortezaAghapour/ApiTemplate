using System;
using System.Collections.Generic;
using System.Text;
using RabitMQTask.Core.Entities.Base;

namespace RabitMQTask.Core.Entities.Weathers
{
    public class Weather   :BaseEntity
    {
        public double Heat { get; set; }
        public string City { get; set; }
    }
}
