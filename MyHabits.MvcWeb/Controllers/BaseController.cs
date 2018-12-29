using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using MyHabits.Common;
using MyHabits.DataEntity;
namespace MyHabits.MvcWeb.Controllers
{
    /// <summary>
    /// 控制器基类，定义一些控制器的公共方法
    /// </summary>
    public class BaseController : Controller
    {

        public string Ip
        {
            get
            {
                return HttpContext.Request.UserHostName;
            }
        }

        /// <summary>
        /// 全局异常捕获
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = Json(new AjaxResult
                {
                    success = false,
                    msg =
                          HttpUtility.HtmlEncode(filterContext.Exception.Message.Trim()
                                                     .Replace("\n", "")
                                                     .Replace("\r", "")
                                                     .Replace("'", "")
                                                 + (filterContext.Exception.InnerException == null
                                                     ? ""
                                                     : "；"
                                                       +
                                                      filterContext.Exception.InnerException.Message.Trim()
                                                           .Replace("\n", "")
                                                           .Replace("\r", "")
                                                           .Replace("'", "")))
                });
            }
            else
            {
                filterContext.Result = View("~/Views/Shared/Error.cshtml", filterContext.Exception);
            }
            LogHelper.Error(filterContext.Exception.Message, filterContext.Exception);
            //标志异常已处理
            filterContext.ExceptionHandled = true;
            //filterContext.HttpContext.Response.Clear();
            //filterContext.HttpContext.Response.StatusCode = 500;
            //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //base.OnException(filterContext);
        }

        /// <summary>
        /// 获取当前登录用户ID
        /// </summary>
        public string CurrentUserID
        {
            get
            {
                if (HttpContext.User.Identity.Name.IsNotNull())
                {
                    return HttpContext.User.Identity.Name;
                }
                return System.Web.HttpContext.Current.Session["uid"] != null ? System.Web.HttpContext.Current.Session["uid"].ToString() : null;
            }
        }

        /// <summary>
        /// 获取当前登录用户详细信息
        /// </summary>
        public UserEntity CurrentUser
        {
            get
            {
                var uname = string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name)
                    ? (System.Web.HttpContext.Current.Session["uid"] ?? "")
                    : HttpContext.User.Identity.Name;
                var userInfo = System.Web.HttpContext.Current.Cache["usr_" + uname] as UserEntity;
                if (userInfo == null)
                {
                    throw new AuthenticationException("您的凭据已经失效，请重新登录。");
                }
                return userInfo;
            }
        }
    }
}