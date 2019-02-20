using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyHabits.DataEntity;
using MyHabits.Business;
using System.Net.Mail;
using System.Net;
using MyHabits.Common;
namespace MyHabits.MvcWeb.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account

        BllAccount bll = new BllAccount();
        public ActionResult Login()
        {
            //var user = bll.GetUserInfo();
            return View();
        }

        public JsonResult Regist(UserEntity user,string emailCode)
        {
            if (Session["EmailCode"]!=null)
            {
                var vCode = Session["EmailCode"].ToString();
                if (!emailCode.Equals(vCode))
                {
                    return Json(new AjaxResult() { success=false,msg="验证码错误"});
                }


                if (bll.UserRegist(user))
                {
                    return Json(new AjaxResult() { success = true, msg = "注冊成功" });
                }
                else
                {
                    return Json(new AjaxResult() { success = false, msg = "注冊失敗" });
                }

            }
            else
            {
                return Json(new AjaxResult() { success = false, msg = "请先获取验证码" });

            }
        }

        public void GetEmailCode(string emailAddress)
        {
            //生成四位随机数做验证码
            Random r = new Random();
            string  code = r.Next(1000, 10000).ToString();

            try
            {
                //发送验证码
                SendEmail(emailAddress, "尊敬的用户,您的验证码为：" + code);

                Session["EmailCode"] = code;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        /// <summary>  
        /// 发送邮件  
        /// </summary>  
        /// <param name="tomail">收件人邮件地址</param>  
        /// <param name="title">标题</param>  
        /// <param name="content">邮件正文</param>  
        public static void SendEmail(string toMail, string content)
        {
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();
            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress("1369540047@qq.com");
            //收件人邮箱地址。
            mailMessage.To.Add(new MailAddress(toMail));
            //邮件标题。
            mailMessage.Subject = "重置密码";
            //邮件内容。
            mailMessage.Body = content;

            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();
            //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
            client.Host = "smtp.qq.com";
            //使用安全加密连接。
            client.EnableSsl = true;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;
            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential("1369540047@qq.com", "izdwgemcwvexbadg");
            //发送
            client.Send(mailMessage);

        }
    }
}