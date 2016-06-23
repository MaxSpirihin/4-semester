using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UltraNews.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {

        public string List()
        {
            return "Hello Admin!!!";
        }

    }
}
