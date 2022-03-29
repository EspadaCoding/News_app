using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Picabu.Models.Identity;
using Picabu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email=model.Email,
                    Fullname=model.FullName,
                    Year = model.Year, 
                };
                var result = await userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user,true);
                    return RedirectToAction("Index","News");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code,error.Description);
                    }

                }
            }
                return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string ReturnUrl)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "News");
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginForm", "Incorrect username or password!");
                }
            }
            else
            {
                ModelState.AddModelError("LoginForm", "This user not exist");
            }
            return View();
        }
        [HttpGet]
        public async  Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "News");
        }
    }

}
