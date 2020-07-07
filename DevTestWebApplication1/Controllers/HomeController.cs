using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DevTestWebApplication1.Models;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DevTestWebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICustomMembership _membership;

        public HomeController(ILogger<HomeController> logger, ICustomMembership membership)
        {
            _logger = logger;
            _membership = membership;
        }

        public IActionResult Index()
        {
            var redirect = _membership.CurrentUser.Roles.FirstOrDefault().DefaultRedirect ?? "/home/logout";
            return Redirect(redirect);
        }

        public IActionResult ClientDashboard()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _membership.Login(model.User, model.Password))
                {
                    return RedirectToAction("index");
                }
                ModelState.AddModelError("Password", "User name/Password combination was not found");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _membership.LogOff();
            return RedirectToAction("login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
