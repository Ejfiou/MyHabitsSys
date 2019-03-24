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
    public class PublishQuestionController : Controller
    {
        // GET: PublishQuestion
        BllQuestion bll = new BllQuestion();
        public ActionResult PublishQuestion()
        {
            return View();
        }


        
        public JsonResult SetQuesInfo(Question ques)
        {
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