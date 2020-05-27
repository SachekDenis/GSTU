using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ComputerStore.DataAccessLayer.Models.Identity;
using ComputerStore.WebAPI.Models;
using ComputerStore.WebUI.AppConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ComputerStore.WebAPI.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly SignInManager<IdentityBuyer> _signInManager;
        private readonly UserManager<IdentityBuyer> _userManager;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(
            UserManager<IdentityBuyer> userManager, 
            SignInManager<IdentityBuyer> signInManager,
            ILogger<AuthorizationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<JwtTokenResult>> Token([FromQuery] Login login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

                var roleClaims = (await _userManager.GetRolesAsync(user)).Select(role => new Claim(ClaimTypes.Role, role));

                var claims = new[]
                             {
                                 new Claim(JwtRegisteredClaimNames.Sub, login.Email),
                                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                 new Claim(JwtRegisteredClaimNames.UniqueName, login.Email),
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

                return BadRequest();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured during creating token. Exception: {exception.Message}");
                return BadRequest();
            }
        }
    }
}