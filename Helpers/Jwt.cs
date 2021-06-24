using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthTestApi.Helpers
{
    public class Jwt
    {
        private string SecureKey = "This is secure key";
        public string Generate(int id)
        {
            var securekey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecureKey));
            var credentials = new SigningCredentials(securekey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(5));

            var securitytoken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securitytoken);
        }
        public JwtSecurityToken VerifyJwt(string jwt)
        {
            var tokenHndlr = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecureKey);

            tokenHndlr.ValidateToken(jwt, new TokenValidationParameters 
            { 
                IssuerSigningKey=new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey=true,
                ValidateIssuer=false,
                ValidateAudience=false
            }, out SecurityToken validateToken);

            return (JwtSecurityToken) validateToken;
        }
    }
}
