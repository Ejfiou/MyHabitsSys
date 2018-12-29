using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyHabits.Common
{
    /// <summary>
    /// 定义了一些公共方法或扩展方法
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 返回html字符串的解码形式字符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string html)
        {
            return !string.IsNullOrWhiteSpace(html) ? HttpUtility.HtmlDecode(html) : null;
        }

        /// <summary>
        /// 返回字符串的html编码形式字符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string html)
        {
            return !string.IsNullOrWhiteSpace(html) ? HttpUtility.HtmlEncode(html) : null;
        }


        /// <summary>
        /// 将对象转化为json字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToJson(this object source)
        {
            try
            {
                return JsonConvert.SerializeObject(source);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 将json字符串转化为指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 等效于string.Format
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Substitute(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
        public static bool IsNull(this object obj)
        {
            if (obj == null) return true;
            if (string.IsNullOrWhiteSpace(obj.ToString()))
                return true;
            else
                return false;
        }
        public static bool IsNotNull(this object obj)
        {
            if (obj == null) return false;
            if (string.IsNullOrWhiteSpace(obj.ToString()))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 忽略大小写比较两个字符串是否相等
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static bool Same(this string str, string str2)
        {
            if (str == null && str2 == null) return true;
            if (str == null || str2 == null) return false;
            return str.Trim().Equals(str2.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
