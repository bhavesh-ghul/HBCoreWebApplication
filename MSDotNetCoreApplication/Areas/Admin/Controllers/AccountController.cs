using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.User;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace MSDotNetCoreApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="_userService">The user service.</param>
        public AccountController(IUserService _userService)
        {
            userService = _userService;
        }

        [Route("admin")]
        [ActionName("Signin")]
        public IActionResult Index()
        {
            return View("Signin");
        }

        [HttpPost]
        [Route("admin")]
        [ActionName("Signin")]
        public IActionResult Index(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View("Signin", userLogin);
            }

            UserProfile user = new UserProfile();
            user = userService.Login(userLogin);
            if(user != null)
            {
                return RedirectToAction("Index", "Home", new { Areas = "Admin" });
            }
            return View("Signin", userLogin);
        }

        [Route("forgotpassword")]
        public IActionResult Forgotpassword()
        {
            return View();
        }
    }
}