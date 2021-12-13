using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRChatApp.Data;
using SignalRChatApp.Models;
using SignalRChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRChatApp.Controllers
{
    public class AccountController : Controller
    {
        private ChatContext db;
        public AccountController(ChatContext shoppingContext)
        {
            db = shoppingContext;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.Include(u => u.Role)
                    .FirstOrDefaultAsync(i => i.Username == model.Login && i.Password == model.Password || i.Email == model.Login && i.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Login or(and) password is(are) incorrect");
            }
            return View(model);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Username == model.Username || u.Email == model.Email);
                if (user == null)
                {
                    user = new User
                    {
                        Email = model.Email,
                        Username = model.Username,
                        Password = model.Password,
                    };
                    Role role = await db.Roles.FirstOrDefaultAsync(i => i.Name == "user");
                    if (role != null)
                        user.RoleId = 2;

                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Incorrect login and(or)password");
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                new Claim("Id",user.Id.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                                                   ClaimsIdentity.DefaultNameClaimType,
                                                   ClaimsIdentity.DefaultRoleClaimType
                                                   );
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
