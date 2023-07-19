using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.Interfaces;
using Jadval.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Threading.Tasks;

namespace Jadval.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticatedUserService authenticatedUserService;

        public AccountController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthenticatedUserService authenticatedUserService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            this.authenticatedUserService = authenticatedUserService;
        }

        public IActionResult LogIn()=> View();

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationRequest model)
        {
            if (ModelState.IsValid)
            {
                var username = model.UserName.Trim();
                var user = await _userManager.FindByNameAsync(username);

                if (user is null)
                {
                    user = await _userManager.Users.FirstOrDefaultAsync(p => p.PhoneNumber == username);
                    if (user is null)
                    {
                        ModelState.AddModelError(nameof(model.UserName), "Username_or_phonenumber_is_incorrect");
                        return View(model);
                    }
                }

                var checkPasswordSignInAsync = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!checkPasswordSignInAsync.Succeeded)
                {
                    ModelState.AddModelError(nameof(model.Password), "Invalid_password");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);

                if (result.Succeeded)
                    return Redirect("/Home");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }

}
