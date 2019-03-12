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
            }
            else
            {
                ViewBag.IsLogin = false;
                ViewBag.logID = "";
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

            if (Session["UserInfo"]!= null)
            {

                int id = (Session["UserInfo"] as UserEntity).ID;
                string userImg = Path.Combine("\\Img\\UserImg", fileName);

                UserEntity user = bll.GetUserById(id);
                if (user != null)
                {
                    user.userImg = userImg;
                    bll.UpdateUserImg(user);
                }
              
            }
            return Json(new AjaxResult() {success = true, msg = Path.Combine(filePath, fileName)});
        }
    }
}