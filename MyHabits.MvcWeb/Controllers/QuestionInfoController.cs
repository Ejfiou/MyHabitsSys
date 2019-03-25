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
    public class QuestionInfoController : Controller
    {
        // GET: QuestionInfo
        BllQuestion bll = new BllQuestion();
        public ActionResult QuestionInfo()
        {
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
    }
}