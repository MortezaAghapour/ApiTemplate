using System;
using System.Collections.Generic;
using System.Text;
using RabitMQTask.Core.DataTransforObjects.Jwt;
using RabitMQTask.Model.Jwt;

namespace RabitMQTask.Mapper.Jwt
{
    public class JwtMapper
    {
        public static JwtReturnViewModel ToJwtReturnViewModel(JwtReturnModel model)
        {
            return new JwtReturnViewModel
            {
                TokenType = model.TokenType,
                ExpiresIn = model.ExpiresIn,
                AccessToken = model.AccessToken,
                RefreshToken = model.RefreshToken
            };
        }
    }
}
