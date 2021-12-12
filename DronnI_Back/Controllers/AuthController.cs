using DronnI_Back.Models;
using DronnI_Back.Models.DbModels;
using DronnI_Back.Helpers;
using DronnI_Back.Models.RequestModels;
using DronnI_Back.Models.ResponseModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace DronnI_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        ApplicationContext appCtx;

        public AuthController(ApplicationContext context)
        {
            appCtx = context;
        }
        [HttpPost("Test")]
        public IActionResult Test()
        {
            List<User> users = appCtx.Users.ToList();
            return Json(users);
        }
        [HttpGet]
        public string Get()
        {
            return "Sure";
        }
        public User AddUser(string login, string password, string email)
        {
            if (appCtx.Users.FirstOrDefault(u => u.Email == email) == null)
            {
                User user = new User {
                    Email = email,
                    Password = Hasher.HashPassword(password),
                    Role = "user",
                    Login = login};
                appCtx.Users.Add(user);
                appCtx.SaveChanges();
                var u = appCtx.Users.ToList().Last();
                appCtx.Entry(u).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appCtx.SaveChanges();

                return user;
            }
            return null;
        }

       

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel us)
        {
            User u = AddUser(us.Login, us.Password, us.Email);
            if (u != null)
            {
                return Json(Authenticate(new UserModel { Email = u.Email, Password = u.Password }));
            }
            return BadRequest(new { errormesage = "This email is already used" });
        }
        
        public AuthResponse Authenticate(UserModel userModel)
        {
            User user = appCtx.Users.ToList().FirstOrDefault(x => x.Email == userModel.Email
            && Hasher.VerifyHashedPassword(x.Password, userModel.Password));
            var identity = GetIdentity(user);
            if (identity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthResponse(user, encodedJwt);
        }
        [HttpPost("authenticate")]
        public IActionResult Authentication([FromBody] UserModel us)
        {
            var a = Authenticate(us);
            if (a == null)
            {
                return new UnauthorizedResult();
            }
            return Json(a);
        }

        private ClaimsIdentity GetIdentity(User u)
        {

            if (u != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, u.Email),
                    new Claim("id", u.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, u.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        } 
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var result = HttpContext.User.Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return Json(result);
        }
    }
}
