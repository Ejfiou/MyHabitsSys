using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.Common
{
    /// <summary>
    /// 异步请求结果的Json对象，默认表示请求成功
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 响应的消息
        /// </summary>
        public string msg { set; get; }

        /// <summary>
        /// 请求是否成功，默认成功
        /// </summary>
        public bool success { set; get; } = true;

        /// <summary>
        /// 返回的Url
        /// </summary>
        public string url { set; get; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object data { set; get; }

        /// <summary>
        /// 请求数据的条数，获取分页列表时使用
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 状态码，自定义
        /// </summary>
        public int code { get; set; }
    }

}