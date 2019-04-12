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
    public class PublishHealController : BaseController
    {
        // GET: PublishHeal
        BllPublishHeal bll = new BllPublishHeal();
        public ActionResult PublishHeal()

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

        [ValidateInput(false)]
        //点击发布按钮 将健康资讯存入数据库中
        public JsonResult Sethealth(PublishHeal heal)
        {
            heal.heal_sdTime = DateTime.Now;
            bool flag = bll.HealthSet(heal);
            if (flag == true)
            {
                return Json(new AjaxResult() { success = true, msg = "塞值成功" });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "塞值失敗" });
            }
        }
        //将数据库中健康资讯的值带入到首页
        public JsonResult GetHealthInfo()
        {
            PublishHeal pub = new PublishHeal();
           List<PublishHeal> listpub =   bll.GetHealthInfo(pub);
            return Json(new AjaxResult() { success = true, data = listpub});
        }
        
        //草稿箱数据
        public JsonResult GetHealthInfo2()
        {
            PublishHeal pub = new PublishHeal();
            List<PublishHeal> listpub = bll.GetHealthInfo2(pub);
            return Json(new AjaxResult() { success = true, data = listpub });
        }
        
        public JsonResult UpdateHealthInfo2(PublishHeal heal)
                {
            if (heal.ID != null)
            {
                bll.UpdateHealthInfo2(heal);
            }
            return Json(new AjaxResult() { success = true, msg = "上传成功" });
        }
        //带回当前id号并通过id搜索对应资讯详情
        /// <param name="healthinfo"></param>
        public JsonResult SetHealthInfo(PublishHeal healthinfo)
        {
            
                PublishHeal pub1 = new PublishHeal();
               
                List<PublishHeal> listpub = bll.SetHealthInfo(healthinfo);
            if (listpub.Count > 0)
            {
                return Json(new AjaxResult() { success = true, msg = "取值成功" ,data = listpub });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "传送失败" });
            }
        }

        public JsonResult GetHealRotation()
        {
            PublishHeal pub = new PublishHeal();
            List<PublishHeal> listpub = bll.GetHealRotation(pub);
            return Json(new AjaxResult() { success = true, data = listpub });
        }





    }
}