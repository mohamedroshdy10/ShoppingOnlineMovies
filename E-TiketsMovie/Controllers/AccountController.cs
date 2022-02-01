using E_TiketsMovie.Data;
using E_TiketsMovie.Data.Static;
using E_TiketsMovie.Models.Tables.Peopel;
using E_TiketsMovie.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<AppliationUser> _userManger;
        private readonly SignInManager<AppliationUser> _signInManager;
        private readonly EcommercdbContext _context;
        #region Fileds
        public AccountController(UserManager<AppliationUser> userManager,
            SignInManager<AppliationUser> signInManager,
            EcommercdbContext context)
        {
            this._userManger = userManager;
            this._signInManager = signInManager;
            this._context = context;
        }
        #endregion
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM mdl)
        {
            if (!ModelState.IsValid) return View(mdl);

            var user = await _userManger.FindByEmailAsync(mdl.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(mdl);
            }

            var newUser = new AppliationUser()
            {
                FullName = mdl.FullName,
                Email = mdl.EmailAddress,
                UserName = mdl.EmailAddress
            };
            var newUserResponse = await _userManger.CreateAsync(newUser, mdl.Password);

            if (newUserResponse.Succeeded)
                await _userManger.AddToRoleAsync(newUser, UserRoles.User);
            return View("RegisterComplte");
        }
        public IActionResult LogIn() => View(new LoginViewModel());
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel mdl)
        {
            if (!ModelState.IsValid) return View(mdl);

            var user = await _userManger.FindByEmailAsync(mdl.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManger.CheckPasswordAsync(user, mdl.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, mdl.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(mdl);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(mdl);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }


        #region Actions
        #endregion
    }
}
