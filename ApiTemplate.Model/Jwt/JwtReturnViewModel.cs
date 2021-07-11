using System;
using System.Collections.Generic;
using System.Text;

namespace RabitMQTask.Model.Jwt
{
    public class JwtReturnViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
    }
}
