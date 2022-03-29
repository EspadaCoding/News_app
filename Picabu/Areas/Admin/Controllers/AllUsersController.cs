using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Areas.Admin.Controllers
{
    public class AllUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
