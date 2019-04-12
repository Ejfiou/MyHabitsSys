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
    public class AnswerinfoController : Controller
    {
        // GET: Answerinfo
        BllAnswerinfo bll = new BllAnswerinfo();
        public ActionResult Answerinfo()
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


        public JsonResult MyAnswerinfo(Answerinfo answer)
        {

            Answerinfo pub1 = new Answerinfo();

            List<Answerinfo> listpub = bll.MyAnswerinfo(answer);
            if (listpub.Count > 0)
            {
                return Json(new AjaxResult() { success = true, msg = "取值成功", data = listpub });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "传送失败" });
            }
        }
        
         public JsonResult AllAnswerinfo(Answerinfo answer)
        {

            Answerinfo pub1 = new Answerinfo();

            List<Answerinfo> listpub = bll.AllAnswerinfo(answer);
            if (listpub.Count > 0)
            {
                return Json(new AjaxResult() { success = true, msg = "取值成功", data = listpub });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "传送失败" });
            }
        }






    }
}