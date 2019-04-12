using MyHabits.Business;
using MyHabits.Common;
using MyHabits.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHabits.MvcWeb.Controllers
{
    public class PersonalListController : Controller
    {
        // GET: PersonalList
        BllAccount bll = new BllAccount();
        public ActionResult PersonalList()
        {
            if (Session["UserInfo"] != null)
            {
                ViewBag.IsLogin = true;
                ViewBag.logID = (Session["UserInfo"] as UserEntity).ID;
                ViewBag.logImg = (Session["UserInfo"] as UserEntity).userImg;
                ViewBag.logStatus = (Session["UserInfo"] as UserEntity).userStatus;
            }
            else
            {
                ViewBag.IsLogin = false;
                ViewBag.logID = "";
                ViewBag.logImg = "";
                ViewBag.logStatus = "";
                return RedirectToRoute(new { controller = "HomePage", action = "HomePage" });

            }
            return View();
        }


        public JsonResult GetPersonalList(int page = 1,int limit=10)
        {
            UserEntity pub = new UserEntity();
 
            List<UserEntity> listpub = bll.GetPersonalList( out int total, page, limit);
            return Json(new AjaxResult() { success = true, data = listpub, count = total });
        }
    }
}