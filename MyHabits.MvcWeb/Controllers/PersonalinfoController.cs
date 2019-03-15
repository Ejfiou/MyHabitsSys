using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyHabits.Common;
using MyHabits.Business;
using MyHabits.DataEntity;
namespace MyHabits.MvcWeb.Controllers
{
    public class PersonalinfoController : BaseController
    {

        // GET: Personalinfo
        public ActionResult Personalinfo()
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


        private BllAccount bll = new BllAccount();

        public JsonResult UploadFile()
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file is null)
            {
                return null;
            }
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "Img\\UserImg"));
            file.SaveAs(Path.Combine(filePath, fileName));

            bool flag = false;

            if (Session["UserInfo"]!= null)
            {
                UserEntity u = (Session["UserInfo"] as UserEntity);
                string userImg = Path.Combine("\\Img\\UserImg", fileName);

                if (u != null)
                {
                    u.userImg = userImg;
                    flag = bll.UpdateUser(u);
                }
                Session["UserInfo"] = u;
            }
            return Json(new AjaxResult() {success = flag, msg = Path.Combine(filePath, fileName)});
        }


        //带回当前id号并通过id搜索对应用戶信息
        public JsonResult GetMyuserInfo(UserEntity user)
        {

            UserEntity pub1 = new UserEntity();

            List<UserEntity> listpub = bll.GetMyuserInfo(user);
            if (listpub.Count > 0)
            {
                return Json(new AjaxResult() { success = true, msg = "取值成功", data = listpub });
            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "传送失败" });
            }
        }
        //将新的个人信息上传到数据库
        public JsonResult UpdateUserInfo(UserEntity user)
        {

            if (Session["UserInfo"] != null)
            {
                UserEntity u = (Session["UserInfo"] as UserEntity);

                if (u != null)
                {
                    u.userAge = user.userAge;
                    u.userEmail = user.userEmail;
                    u.userSex = user.userSex;
                    u.userPhone = user.userPhone;
                    u.userQQ = user.userQQ;
                    u.nickName = user.nickName;
                    bll.UpdateUser(u);
                }
                Session["UserInfo"] = u;
            }
            return Json(new AjaxResult() { success = true, msg = "上传成功" });
        }


        public JsonResult UserPsd(UserEntity user)
        {
            UserEntity u = (Session["UserInfo"] as UserEntity);
            UserEntity pub1 = new UserEntity();
            user.ID = u.ID;
            List<UserEntity> listpub = bll.UserPsd(user);
            if (listpub.Count > 0)
            {


                return Json(new AjaxResult() { success = true, msg = listpub[0].userImg });
            }
            else
            {
                Session["UserInfo"] = null;
                return Json(new AjaxResult() { success = false, msg = "用户名不存在" });
            }
        }

        public JsonResult UpdatePwdInfo(UserEntity user)
        {

            if (Session["UserInfo"] != null)
            {
                UserEntity u = (Session["UserInfo"] as UserEntity);

                if (u != null)
                {
                    u.password = user.password;
                    bll.UpdatePwd(u);
                }
                Session["UserInfo"] = u;
            }
            return Json(new AjaxResult() { success = true, msg = "上传成功" });
        }


    }
}