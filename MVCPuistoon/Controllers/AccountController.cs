using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCPuistoon.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
namespace MVCPuistoon
{
    public class AccountController : Controller
    {
        private readonly UserManager<Käyttäjä> userManager; public AccountController(UserManager<Käyttäjä> userManager)
        {
            this.userManager = userManager;
        }
     
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.KäyttäjäTunnus); if (user == null)
                {
                    user = new Käyttäjä
                    {
                        Id = Guid.NewGuid().ToString(),
                        KäyttäjäTunnus = model.KäyttäjäTunnus
                    }; var result = await userManager.CreateAsync(user, model.Salasana);
                    return View("Login");
                }
                ModelState.AddModelError("", "Tämä käyttäjätunnus on jo olemassa.");
            }
            
            return View();
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page."; return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.KäyttäjäTunnus); if (user != null && await userManager.CheckPasswordAsync(user, model.Salasana))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.KäyttäjäTunnus)); await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity)); 
                    return RedirectToAction("Index", "Tapahtumat");
                }
                ModelState.AddModelError("", "Väärä käyttäjä tai salasana");
            }
            return View();
        }
    }
}