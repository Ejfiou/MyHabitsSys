using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyHabits.DataEntity;

namespace MyHabits.MvcWeb.Controllers
{
    public class HomePageController : BaseController
    {
        // GET: HomePage
        public ActionResult HomePage()
        {
            if (Session["UserInfo"] != null)
            {
                ViewBag.IsLogin = true;
                ViewBag.logID = (Session["UserInfo"] as UserEntity).ID;
            }
            else
            {
                ViewBag.IsLogin = false;
                ViewBag.logID = "";
            }
            return View();
        }
    }
}