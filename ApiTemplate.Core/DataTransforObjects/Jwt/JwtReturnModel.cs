namespace ApiTemplate.Core.DataTransforObjects.Jwt
{
    public class JwtReturnModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
    }
}
