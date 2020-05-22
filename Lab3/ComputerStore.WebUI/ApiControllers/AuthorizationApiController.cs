using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ComputerStore.DataAccessLayer.Models.Identity;
using ComputerStore.WebUI.Models;
using ComputerStore.WebUI.Models.JwtToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ComputerStore.WebUI.ApiControllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationApiController : ControllerBase
    {
        private readonly SignInManager<IdentityBuyer> _signInManager;
        private readonly UserManager<IdentityBuyer> _userManager;

        public AuthorizationApiController(UserManager<IdentityBuyer> userManager, 
                                          SignInManager<IdentityBuyer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("createtoken")]
        public async Task<JwtTokenResult> CreateToken([FromQuery] LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);

            var roleClaims = (await _userManager.GetRolesAsync(user)).Select(role => new Claim(ClaimTypes.Role, role));

            var claims = new[]
                         {
                             new Claim(JwtRegisteredClaimNames.Sub, loginViewModel.Email),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             new Claim(JwtRegisteredClaimNames.UniqueName, loginViewModel.Email),
                             new Claim(BuyerClaim.BuyerId, user.BuyerId.ToString()), 
                         };

            claims = claims.Concat(roleClaims).ToArray();

            if (result.Succeeded)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(JwtInfo.Issuer, JwtInfo.Audience, claims, expires: DateTime.Now.AddHours(1), signingCredentials: creds);

                var tokenResult = new JwtTokenResult
                                  {
                                      Token = new JwtSecurityTokenHandler().WriteToken(token)
                                  };

                return tokenResult;
            }

            return null;
        }
    }
}