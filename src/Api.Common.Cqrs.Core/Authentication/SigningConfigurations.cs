using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Common.Core.Authentication
{
    public class SigningConfigurations
    {
        public SigningConfigurations()
        {
            var keyBytes = Encoding.ASCII.GetBytes("Api.Template.Authentication");
            var signingKey = new SymmetricSecurityKey(keyBytes);

            Key = signingKey;

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256);
        }

        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
    }
}