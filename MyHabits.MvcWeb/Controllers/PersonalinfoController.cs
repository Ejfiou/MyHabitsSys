using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHabits.MvcWeb.Controllers
{
    public class PersonalinfoController : BaseController
    {
        // GET: Personalinfo
        public ActionResult Personalinfo()
        {
            return View();
        }
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
            return null;
        }
    }
}