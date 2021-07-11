using ApiTemplate.Core.Entities.Base;

namespace ApiTemplate.Core.Entities.Weathers
{
    public class Weather   :BaseEntity
    {
        public double Heat { get; set; }
        public string City { get; set; }
    }
}
