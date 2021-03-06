﻿using MyHabits.Business;
using MyHabits.Common;
using MyHabits.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHabits.MvcWeb.Controllers
{
    public class PublishQuestionController : Controller
    {
        // GET: PublishQuestion
        BllQuestion bll = new BllQuestion();
        public ActionResult PublishQuestion()
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


        
        public JsonResult SetQuesInfo(Question ques)
        {
            ques.question_sdTime = DateTime.Now;
            bool flag = bll.SetQuesinfo(ques);
            if (flag == true)
            {
                return Json(new AjaxResult() { success = true, msg = "问卷发布成功" });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "问卷发布失败" });
            }
        }
    }
}