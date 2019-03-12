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


        //带回当前id号并通过id搜索对应用戶信息
        public JsonResult SetMyuserInfo(UserEntity user)
        {

            UserEntity pub1 = new UserEntity();

            List<UserEntity> listpub = bll.SetMyuserInfo(user);
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