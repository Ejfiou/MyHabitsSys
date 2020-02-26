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
    public class HealListController : Controller
    {
        // GET: HealList
        BllPublishHeal bll = new BllPublishHeal();
        public ActionResult HealList()
        {
            if (Session["UserInfo"] != null)
            {
                ViewBag.IsLogin = true;
                ViewBag.logID = (Session["UserInfo"] as UserEntity).ID;
                ViewBag.logImg = (Session["UserInfo"] as UserEntity).userImg;
                ViewBag.logStatus = (Session["UserInfo"] as UserEntity).userStatus;
            }
            return View();
        }
        //数据库资讯列表
        public JsonResult GetHealList(PublishHeal heal, int pageSize=5,int page = 1)
        {
            PublishHeal pub = new PublishHeal();

            List<PublishHeal> listpub = bll.GetHealList(heal,out int total,pageSize,page);
            return Json(new AjaxResult() { success = true, data = listpub , count = total});
        }

        public JsonResult GetHealNotop()
        {

            PublishHeal pub1 = new PublishHeal();
            List<PublishHeal> listpub = bll.GetHealNotop(pub1);
            if (listpub.Count > 0)
            {
                return Json(new AjaxResult() { success = true, msg = "取值成功", data = listpub });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "传送失败" });
            }
        }

        public JsonResult GetHealTip(string[] tip)
        {


            string title = string.Empty;

            for (int i = 0; i < tip.Length; i++)
            {
                if (i == 0)
                {
                    title += $" heal_title like '%{tip[i]}%' ";
                }
                else
                {
                    title += $" or heal_title like '%{tip[i]}%' ";
                }

            }

            List<PublishHeal> listpub = bll.GetHealTip(title);
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