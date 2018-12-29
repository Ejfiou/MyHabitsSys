using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyHabits.DataEntity;
using MyHabits.Business;
namespace MyHabits.MvcWeb.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account

        BllAccount bll = new BllAccount();
        public ActionResult Login()
        {
            var user = bll.GetUserInfo();
            return View();
        }
    }
}