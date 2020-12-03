using bMovieTracker.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bMovieTracker.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MovieTrackerUserManager _userManager;


        public AccountController(MovieTrackerUserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return new ContentResult() { Content = "account controller" };
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest();

            var user = await _userManager.FindByNameAsync(username);
            if (user != null && _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed)
            {
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: JwtAuthOptions.ISSUER,
                        audience: JwtAuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: new Claim[] {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                        },
                        expires: now.Add(TimeSpan.FromMinutes(JwtAuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(JwtAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = user.UserName
                };

                return new JsonResult(response);
            }
            return BadRequest();
        }
    }
}