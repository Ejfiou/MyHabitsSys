using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyHabits.Business;
using MyHabits.DataEntity;
namespace MyHabits.MvcWeb
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        private BllAccount bll = new BllAccount();


        [WebMethod]
        public void IsLoginIdRepeat(string txtUserName)
        {
            //string txtUserName = HttpContext.Current.Request["txtUserName"];

            UserEntity user = new UserEntity()
            {
                userName = txtUserName
            };

            var  userList = bll.GetUserInfo(user);
            HttpContext.Current.Response.Write(userList.Count>0);
        }
    }
}
