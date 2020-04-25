using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MSDotNetCoreApplication.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Route("Dashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}