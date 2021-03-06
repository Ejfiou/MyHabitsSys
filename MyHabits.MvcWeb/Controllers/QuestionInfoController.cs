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
    public class QuestionInfoController : Controller
    {
        // GET: QuestionInfo
        BllQuestion bll = new BllQuestion();
        BllAnswerinfo bllan = new BllAnswerinfo();
        public ActionResult QuestionInfo()
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

        public JsonResult GetQuesInfo(Question ques)
        {

            Question pub1 = new Question ();

            List<Question> listpub = bll.GetQuesInfo(ques);
            if (listpub.Count > 0)
            {
                return Json(new AjaxResult() { success = true, msg = "取值成功", data = listpub });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "传送失败" });
            }
        }


        
        public JsonResult SetAnswerinfo(Answerinfo answer)
        {

            Answerinfo pub1 = new Answerinfo();

            bool flag = bllan.SetAnswerinfo(answer);
            if (flag == true)
            {
                return Json(new AjaxResult() { success = true, msg = "传值成功"});
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "传值失败" });
            }
        }

        public JsonResult GetAnswerinfo(Answerinfo answer)
        {

            Answerinfo pub1 = new Answerinfo();

            bool flag = bllan.GetAnswerinfo(answer);
            if (flag == true)
            {
                return Json(new AjaxResult() { success = true, msg = "传值成功" });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "传值失败" });
            }
        }

    }
}