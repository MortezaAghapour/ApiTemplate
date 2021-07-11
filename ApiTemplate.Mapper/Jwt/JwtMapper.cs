using ApiTemplate.Core.DataTransforObjects.Jwt;
using ApiTemplate.Model.Jwt;

namespace ApiTemplate.Mapper.Jwt
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
