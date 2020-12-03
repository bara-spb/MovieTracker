using bMovieTracker.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bMovieTracker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MovieTrackerUserManager _userManager;


        public AccountController(MovieTrackerUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Login user to system
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "username": "myUsername",
        ///        "password": "myPassword"
        ///     }
        ///
        /// </remarks>
        /// <param name="username">User's email</param>  
        /// <param name="password">User's password</param>  
        /// <response code="200">JWT token to authentificate</response>
        /// <response code="400">Please, specify username and password</response>
        /// <response code="401">No account with this username/password combination was found</response> 
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest("Please, specify username and password");

            var user = await _userManager.FindByNameAsync(username);
            if (user != null && _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed)
            {
                return new JsonResult(CreateToken(user));
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new { message = "No account with this username/password combination was found" });
            }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "email": "myEmail",
        ///        "password": "myPassword"
        ///     }
        ///
        /// </remarks>
        /// <param name="email">User's email</param>  
        /// <param name="password">User's password</param>  
        /// <response code="200">JWT token to authentificate</response>
        /// <response code="400">Please, specify email and password</response>
        /// <response code="500">This Email is already registered</response> 
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return BadRequest("Please, specify email and password");

            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "This Email is already registered" });

            var newUser = new MovieTrackerUser() {
                UserName = email,
                Email = email
            };

            var registrationResult = await _userManager.CreateAsync(newUser, password);
            if (registrationResult.Succeeded)
            {
                return new JsonResult(CreateToken(newUser));
            }
            return BadRequest(registrationResult.Errors.FirstOrDefault()?.Description);
        }

        private object CreateToken(MovieTrackerUser user)
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
            return response;
        }

    }
}