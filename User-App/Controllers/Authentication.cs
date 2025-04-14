using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using User_App.Helpers;
using User_App.Models;
using User_App.Models.Authentication;

namespace User_App.Controllers
{
    public class Authentication : Controller
    {
        private readonly UserappContext userAppContext;
        private readonly IConfiguration configuration;


        public Authentication(UserappContext context, IConfiguration _configuration)
        {
            userAppContext = context;
            configuration = _configuration;
        }
        // POST: Authentication/Create
        [HttpPost("login")]
        public IActionResult login([FromBody] UserAuthenticationModel authRequestData)
        {
            if (string.IsNullOrWhiteSpace(authRequestData.UserName) || string.IsNullOrWhiteSpace(authRequestData.Password))
                return BadRequest(new { message = "Username and password are required." });

            var user = userAppContext.OrUsers.FirstOrDefault(userDetail => userDetail.UserName == authRequestData.UserName);

            if(user != null)
            {
                if(user.PasswordHash == User_App.Helpers.PasswordHasher.Hash(authRequestData.Password)) {
                    var token = JwtTokenHelper.GenerateToken(user.UserName, user.UserId, configuration);
                    return Ok(new
                    {
                    message = "Login successful",
                    token,
                    user = user
                    });       
                }
                else
                {
                    return Unauthorized("Invalid user name or password");
                }
            } else
            {
                return BadRequest(new { message = "Invalid User." });
            }
        }
    }
}
