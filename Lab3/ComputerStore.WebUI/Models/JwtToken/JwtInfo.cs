using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ComputerStore.WebUI.Models.JwtToken
{
    public class JwtInfo
    {
        public const string Issuer = "Store";
        public const string Audience = "ApiUser";
        public const string Key = "1234567890123456";
        public const string AuthSchemes = "Identity.Application" + "," + JwtBearerDefaults.AuthenticationScheme;
    }
}