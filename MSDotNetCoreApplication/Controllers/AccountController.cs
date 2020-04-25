using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.User;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace MSDotNetCoreApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="_userService">The user service.</param>
        public AccountController(IUserService _userService)
        {
            userService = _userService;
        }

        public IActionResult Signup()
        {
            return View("signup");
        }

        [HttpPost]
        public IActionResult Signup(UserProfile users)
        {
            if (!ModelState.IsValid)
            {
                return View(users);
            }
            userService.InsertUser(users);
            return RedirectToAction("Signin", "Account", new { area = "Admin" });
        }

        public JsonResult IsUserNameAvailable(string userName)
        {
            return Json(!userService.CheckUser(userName));
        }
    }
}