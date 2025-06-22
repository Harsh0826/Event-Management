using Microsoft.AspNetCore.Mvc;
using EventManagement.Data;
using EventManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace EventManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly EventContext _context;

        public AccountController(EventContext context) => _context = context;

        public IActionResult Login() => View();
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("Index", "Event");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Register(string fullname, string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View();
            }

            if (_context.Users.Any(u => u.UserName == username))
            {
                ModelState.AddModelError("", "Username already taken.");
                return View();
            }

            if (_context.Users.Any(u => u.Email == email))
            {
                ModelState.AddModelError("", "Email already registered.");
                return View();
            }

            var newUser = new User
            {
                FullName = fullname,
                UserName = username,
                Email = email,
                Password = password
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
