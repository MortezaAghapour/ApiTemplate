using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Core.Configurations.Jwt;
using ApiTemplate.Core.DataTransforObjects.Jwt;
using ApiTemplate.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ApiTemplate.Service.Jwt
{
    public class JwtService : IJwtService ,IScopedDependency
    {
        #region Fields

        private readonly JwtConfiguration _jwtConfiguration;
        private readonly UserManager<AppUser> _userManager;
        #endregion
        #region Constructors
        public JwtService(JwtConfiguration jwtConfiguration, UserManager<AppUser> userManager)
        {
            _jwtConfiguration = jwtConfiguration;
            _userManager = userManager;
        }
        #endregion
        #region Methods
        public async Task<JwtReturnModel> Generate(AppUser user)
        {
            var secreteKey = Encoding.UTF8.GetBytes(_jwtConfiguration.SecreteKey);
            var signInCredential =
                new SigningCredentials(new SymmetricSecurityKey(secreteKey), SecurityAlgorithms.HmacSha256);


            var encryptionKey = Encoding.UTF8.GetBytes(_jwtConfiguration.EncryptKey);
            var encryptionCredential = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim(ClaimTypes.Name, user.UserName)   ,
                new Claim( new ClaimsIdentityOptions().SecurityStampClaimType, user.SecurityStamp)
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                EncryptingCredentials = encryptionCredential,
                SigningCredentials = signInCredential,
                Audience = _jwtConfiguration.Audience,
                Expires = DateTime.Now.AddMinutes(_jwtConfiguration.ExpirationMinutes),
                NotBefore = DateTime.Now.AddMinutes(_jwtConfiguration.NotBeforeMinutes),
                IssuedAt = DateTime.Now,
                Issuer = _jwtConfiguration.Issuer,
                Subject = new ClaimsIdentity(claims),
                CompressionAlgorithm = CompressionAlgorithms.Deflate,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);
            var jwt = tokenHandler.WriteToken(jwtSecurityToken);

            return new JwtReturnModel
            {
                AccessToken = jwt,
                TokenType = "Bearer",
                ExpiresIn = (int)(jwtSecurityToken.ValidTo - DateTime.UtcNow).TotalSeconds
            };
        }
        #endregion

    }
}