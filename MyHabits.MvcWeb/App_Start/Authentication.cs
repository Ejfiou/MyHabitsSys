using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyHabits.Common;
using MyHabits.DataEntity;
using MyHabits.MvcWeb.Controllers;

using MyHabits.Business;
namespace Baiyun.MvcWeb
{
    /// <summary>
    /// 提供基于Cookie的认证服务
    /// </summary>
    public static class Authentication
    {
        public static BllAccount bllAccount = new BllAccount();

        /// <summary>
        /// 验证用户提供的凭据信息，对用户的身份进行认证。
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="rememberMe">是否使用Cookie记住凭据信息</param>
        /// <param name="isWeb">是否网页登录</param>
        public static UserEntity Authenticate(string userName, string password, bool rememberMe, bool isWeb)
        {
            string pwdhash = password.MD5_32().Encrypt();
            var userInfo = bllAccount.GetUserInfo(userName);
            if (userInfo == null)
            {
                throw new AuthenticationException("没有这个账号");
            }
            if (userInfo == null || userInfo.Password != pwdhash)
            {
                throw new AuthenticationException("您输入的用户名或密码不正确，请重试！");
            }
            if (isWeb)
            {
                SetOnline(userInfo.UserID);
            }
            HttpContext.Current.Session["uid"] = userInfo.UserID;
            HttpContext.Current.Cache["usr_" + userInfo.UserID] = userInfo;
            FormsAuthentication.SetAuthCookie(userInfo.UserID, false);
            if (rememberMe)
            {
                SaveCredential(userInfo.UserID, password);
            }

            else
            {
                ClearCredential();
            }
            return userInfo;
        }

        /// <summary>
        /// 使用请求中的票据进行认证。
        /// </summary>
        /// <param name="ticket">请求的票据</param>
        public static void Authenticate(FormsAuthenticationTicket ticket)
        {
            var userInfo = bllAccount.GetUserInfo(ticket.Name);
            if (userInfo == null)
                throw new AuthenticationException("您的凭据已经失效，请重新登录。");
            HttpContext.Current.Cache["usr_" + ticket.Name] = userInfo;
        }

        /// <summary>
        /// 将用户的凭据使用DES加密后保存到浏览器的Cookie中。
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        private static void SaveCredential(string userName, string password)
        {
            var issueDay = DateTime.Now;
            var expires = issueDay + TimeSpan.FromDays(365);
            var credential = new FormsAuthenticationTicket(1, userName, issueDay, expires, true, password);
            var cookie = HttpContext.Current.Request.Cookies["x-auth"];
            if (cookie == null)
            {
                cookie = new HttpCookie("x-auth", FormsAuthentication.Encrypt(credential).Encrypt()) { Expires = expires };
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            else
            {
                cookie.Value = FormsAuthentication.Encrypt(credential).Encrypt();
                cookie.Expires = expires;
                HttpContext.Current.Response.SetCookie(cookie);
            }
        }

        /// <summary>
        /// 删除Cookie中的凭据信息。
        /// </summary>
        private static void ClearCredential()
        {
            var cookie = new HttpCookie("x-auth", null);
            HttpContext.Current.Response.SetCookie(cookie);
        }

        /// <summary>
        /// 获取当前Cookie中的凭据信息。如果没有信息或信息不正确，将返回null。
        /// </summary>
        /// <returns></returns>
        public static FormsAuthenticationTicket ReadCurrentCredential()
        {
            var cookie = HttpContext.Current.Request.Cookies["x-auth"];
            if (string.IsNullOrEmpty(cookie?.Value)) return null;
            try
            {
                return FormsAuthentication.Decrypt(cookie.Value.Decrypt());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取是否拥有该功能的授权
        /// </summary>
        /// <param name="controller">控制器名称</param>
        /// <param name="action">Action方法名</param>
        /// <param name="name">功能名称或描述，Action方法共用的时候需要传入区分</param>
        /// <returns></returns>
        //public static bool IsAllow(string controller, string action, string name = null)
        //{
        //    var userinfo = HttpContext.Current.Cache["usr_" + HttpContext.Current.User.Identity.Name] as UserEntity ?? new UserEntity();
        //    var allow = name.IsNull() ? userinfo.ActionList.Exists(x => x.Controller.Same(controller) && x.Action.Same(action))
        //        : userinfo.ActionList.Exists(x => x.Controller.Same(controller) && x.Action.Same(action) && x.Name.Same(name));
        //    return allow;
        //}

        /// <summary>
        /// 根据当前权限控制功能按钮的显示或隐藏
        /// </summary>
        /// <returns></returns>
        //public static string DisplayMode(string controller, string action, string name = null)
        //{
        //    return IsAllow(controller, action, name) ? "" : "style='display:none'";
        //}

        /// <summary>
        /// 构造userId为key，sessionid为value的键值对集合，用于检测重复登录
        /// </summary>
        /// <param name="userId"></param>
        private static void SetOnline(string userId)
        {
            var singleOnline = (Hashtable)HttpContext.Current.Application["Online"] ?? new Hashtable();
            if (singleOnline.ContainsKey(userId))
            {
                singleOnline[userId] = HttpContext.Current.Session.SessionID;
            }
            else
                singleOnline.Add(userId, HttpContext.Current.Session.SessionID);

            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application["Online"] = singleOnline;
            HttpContext.Current.Application.UnLock();
        }

        /// <summary>
        /// 手动退出清除sessionid
        /// </summary>
        public static void ClearSession()
        {
            try
            {
                var singleOnline = (Hashtable)HttpContext.Current.Application["Online"];
                if (HttpContext.Current.User.Identity.Name != null && singleOnline?[HttpContext.Current.User.Identity.Name] != null)
                {
                    singleOnline.Remove(HttpContext.Current.User.Identity.Name);
                    HttpContext.Current.Application.Lock();
                    HttpContext.Current.Application["Online"] = singleOnline;
                    HttpContext.Current.Application.UnLock();
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// 记录操作日志到数据库
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="content"></param>
        /// <param name="logType"></param>
        /// <param name="operId"></param>
        //public static void AddLog(string keyword, string content, LogType logType = LogType.系统日志, string operId = null)
        //{
        //    try
        //    {
        //        var logService = DependencyResolver.Current.GetService<IOpLogService>();
        //        var key =
        //            logService.Add(new OpLogEntity()
        //            {
        //                KeyWord = keyword,
        //                Content = content,
        //                OpTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        //                Operator = operId ?? HttpContext.Current.User.Identity.Name,
        //                OpType = logType.ToString()
        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error("写入数据库日志出错！", ex);
        //    }
        //}

    }
}