using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHabits.DataEntity;

namespace MyHabits.DataAccess
{
    public class DalAccount
    {
        public UserEntity GetUserInfo(object uid)
        {
            string sql = "select * from emp where 1=1";
            var user = DbHelper.Query<UserEntity>(sql).FirstOrDefault();
            return user;
        }
    }
}
