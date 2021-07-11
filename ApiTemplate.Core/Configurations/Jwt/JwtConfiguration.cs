using System;
using System.Collections.Generic;
using System.Text;
using RabitMQTask.Common.Markers.Configurations;

namespace RabitMQTask.Core.Configurations.Jwt
{
    public class JwtConfiguration :IAppSetting
    {
        public string SecreteKey { get; set; }
        public string EncryptKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
