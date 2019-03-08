using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHabits.MvcWeb.Controllers
{
    public class PersonalinfoController : Controller
    {
        // GET: Personalinfo
        public ActionResult Personalinfo()
        {
            return View();
        }
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "File"));
            file.SaveAs(Path.Combine(filePath, fileName));
            return View();
        }
    }
}