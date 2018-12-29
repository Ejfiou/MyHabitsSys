//=============================================================== 
// filename :AuthenticationException  
// created by herb at 2018/7/5 14:11:37  
// personal homepage:http://www.xiaoboke.net  
//===============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.Common
{
    /// <summary>
    /// 表示认证用户的凭据是时发生的错误。
    /// </summary>
    public class AuthenticationException : ApplicationException
    {
        public AuthenticationException() { }

        public AuthenticationException(string message) : base(message)
        {

        }

        public AuthenticationException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
