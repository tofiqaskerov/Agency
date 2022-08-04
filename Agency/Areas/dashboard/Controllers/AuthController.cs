﻿using Agency.Areas.dashboard.DTOs;
using Agency.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    public class AuthController : Controller
    {
        private readonly UserManager<M001User> _userManager;
        private readonly SignInManager<M001User> _signInManager;

        public AuthController(UserManager<M001User> userManager, SignInManager<M001User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
        
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return View();
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false,false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
           
            return View();
        }
        public IActionResult Register()
        {   
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            M001User user = new()
            {
                UserName = model.Email, //bu mutleq yazilmalidir
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                FullName = model.FirstName + " " + model.LastName,
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");   
        }
    }
}
