using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHabits.MvcWeb.Controllers
{
    public class HomepageInfoController : BaseController
    {
        // GET: HomepageInfo
        public ActionResult HomepageInfo()
        {
            return View();
        }
    }
}