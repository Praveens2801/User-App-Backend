//using Microsoft.AspNetCore.Mvc;

//namespace User_App.Controllers
//{
//    public class UserController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_App.Models;
using User_App.Models.Users;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserappContext userAppContext;

    public UserController(UserappContext context)
    {
        userAppContext = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel userData)
    {
        if (await userAppContext.OrUsers.AnyAsync(userFromDb => userFromDb.FullName == userData.FullName)) {
            return Conflict(new { message = "Username already exists." });
        }
        var newUser = new OrUser
        {
            OrganizationId = userData.OrganizationId,
            FullName = userData.FullName,
            Email = userData.Email,
            CreatedOn = userData.CreatedOn,
            UserName = userData.UserName,
            PasswordHash = User_App.Helpers.PasswordHasher.Hash(userData.PasswordHash),
            Organization = userData.Organization,
            UserId = userData.UserId,
        };
        userAppContext.OrUsers.Add(newUser);
        await userAppContext.SaveChangesAsync();
        return Ok(new { message = "User registered successfully"});

    }
}