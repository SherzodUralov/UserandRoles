using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserandRoles.Entityes;
using UserandRoles.Models;
using UserandRoles.ViewModels;

namespace UserandRoles.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepo repo;

        public AccountController(IUserRepo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }
        [HttpGet]
        public ViewResult Denied() 
        {
            return View();
        }

        [HttpGet]
        public ViewResult Register() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            await repo.CreateAsync(model);

                 HttpSiginAsyncreg(model);

            return RedirectToAction("Index", "Account");
        }
        [HttpGet("Login")]
        public ViewResult Login() 
        {
            return View();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            var user = await repo.UserReturn(model);
            if (user == null)
                return null;

            var verif = await repo.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt);
            if (!verif)
                return null;

                   HttpSiginAsync(user);

            return RedirectToAction("Index", "Account");

        }
        [Authorize(Roles = "Superadmin, Admin")]
        public ViewResult Index() 
        {
            var model = repo.GetAll();
            return View(model);
        }

        private void HttpSiginAsync(User user) 
        {
            var clamis = new List<Claim>();
            clamis.Add(new Claim(ClaimTypes.Name, user.Email));
            clamis.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var item in repo.gets())
            {

                if (user.UserName == item.UserName)
                {
                clamis.Add(new Claim(ClaimTypes.Role, "Superadmin"));
                clamis.Add(new Claim(ClaimTypes.Role, "Admin"));
                clamis.Add(new Claim(ClaimTypes.Role, "User"));
                }

            }
            foreach (var item in repo.getss())
            {

                if (user.UserName == item.UserName)
                {
                    clamis.Add(new Claim(ClaimTypes.Role, "Admin"));
                    clamis.Add(new Claim(ClaimTypes.Role, "User"));
                }

            }

            //if (user.UserName == "sherzoduralov01@gmail.com")
            //{
            //    clamis.Add(new Claim(ClaimTypes.Role, "Superadmin"));
            //    clamis.Add(new Claim(ClaimTypes.Role, "Admin"));
            //    clamis.Add(new Claim(ClaimTypes.Role, "User"));
            //}
            //else if (user.UserName == "JurabekJalolov98@gmail.com") 
            //{
            //    clamis.Add(new Claim(ClaimTypes.Role, "Admin"));
            //    clamis.Add(new Claim(ClaimTypes.Role, "User"));
            //}

            var claimsIdentity = new ClaimsIdentity(clamis,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrinsipal = new ClaimsPrincipal(claimsIdentity);

          

            HttpContext.SignInAsync(claimsPrinsipal);
        }

        private void HttpSiginAsyncreg(RegisterViewModel model)
        {
            var clamis = new List<Claim>();
            clamis.Add(new Claim(ClaimTypes.Name, model.Email));
            clamis.Add(new Claim(ClaimTypes.NameIdentifier, model.Email));

            var claimsIdentity = new ClaimsIdentity(clamis,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrinsipal = new ClaimsPrincipal(claimsIdentity);

            HttpContext.SignInAsync(claimsPrinsipal);
        }
    }
}
