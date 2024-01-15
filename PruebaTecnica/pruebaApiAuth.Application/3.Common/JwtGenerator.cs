using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pruebaApiAuth.Application._3.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtGenerator
    {
        /// <summary>
        /// Metodo que genera un jwt para su respectiva sesion.
        /// </summary>
        /// <param name="pUsername"></param>
        /// <returns></returns>
        public static async Task<string> GenerateToken(string pUsername)
        {
            //1.Get the configuration appseting.json
            var key = JwtSetting.key;
            var audienceToken = JwtSetting.AudienceToken;
            var issuerToken = JwtSetting.IssuerToken;
            var expireToken = JwtSetting.Expire;

            //2.Secrey key
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //3.create a claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, pUsername),
            });

            //4.Date expired
            var notBefore = DateTime.UtcNow;
            var expires = DateTime.UtcNow;
            var dateTimeExpited = expires.AddMinutes(Convert.ToInt32(expireToken));

            //5.create token to the user
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: notBefore,
                expires: dateTimeExpited,
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            return jwtTokenString;
        }
    }
}
