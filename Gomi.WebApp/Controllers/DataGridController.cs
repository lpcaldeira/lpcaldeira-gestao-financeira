using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gomi.WebApp.Controllers
{
    public class DataGridController : Controller
    {
        public IActionResult MasterDetailView()
        {
            return View();
        }
    }
}