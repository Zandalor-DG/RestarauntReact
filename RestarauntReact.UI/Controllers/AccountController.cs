namespace RestarauntReact.UI.Controllers
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using RestarauntReact.Core;
    using RestarauntReact.Core.Entities;
    using RestarauntReact.Core.Entities.RegisterModel;
    using RestarauntReact.UI.Models.UserVM;

    #endregion

    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return Json(model);

            var user = NHibernateRepositories.GetSingleOrDefault<User>(r => r.Email == model.Email);
            if (user == null)
            {
                user = NHibernateRepositories.SaveOrUpdate(new User { Email = model.Email, Password = model.Password, Role = Role.User, Name = model.Name });

                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return Json(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return Json(model);

            var user = NHibernateRepositories.GetSingleOrDefault<User>(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return Json(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
                         {
                                 new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                                 new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                         };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                                        ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult PersonalArea()
        {
            var id = int.Parse(User.Identity.Name);

            var user = NHibernateRepositories.GetSingleOrDefault<User>(a => a.Id == id);
            var userVm = new UserVM
                         {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Role = Role.User,
                                 Email = user.Email,
                                 Password = user.Password
                         };

            return Json(userVm);
        }

        [HttpPost]
        public IActionResult PersonalArea(UserVM userVm)
        {
            if (!ModelState.IsValid)
                return Json(userVm);

            var user = NHibernateRepositories.GetSingleOrDefault<User>(a => a.Id == userVm.Id);
            user.Name = userVm.Name;
            user.Email = userVm.Email;
            user.Password = userVm.Password;
            user.Role = userVm.Role;
            NHibernateRepositories.SaveOrUpdate<User>(user);
            return RedirectToAction("PersonalArea", "Account");
        }
    }
}