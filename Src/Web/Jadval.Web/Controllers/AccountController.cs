using Jadval.Application.DTOs.Account.Requests;
using Jadval.Application.Interfaces;
using Jadval.Application.Interfaces.UserInterfaces;
using Jadval.Application.Wrappers;
using Jadval.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Jadval.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticatedUserService authenticatedUserService;
        private readonly IGetUserServices getUserServices;

        public AccountController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthenticatedUserService authenticatedUserService, IGetUserServices getUserServices)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            this.authenticatedUserService = authenticatedUserService;
            this.getUserServices = getUserServices;
        }

        public IActionResult LogIn() => View();

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
                    return Redirect("/Crossword");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Start()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Crossword");
            var user = new ApplicationUser()
            {
                UserName = GenerateRandomString(7)
            };
            await _userManager.CreateAsync(user);
            await _signInManager.SignInAsync(user, true);

            return Redirect("/Crossword");

            string GenerateRandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var random = new Random();
                var result = new StringBuilder(length);

                for (int i = 0; i < length; i++)
                {
                    int index = random.Next(chars.Length);
                    result.Append(chars[index]);
                }

                return result.ToString();
            }

        }

        [Authorize]
        public async Task<Result<long>> GetCoins() =>await  getUserServices.GetUserCoins();
    }
}
