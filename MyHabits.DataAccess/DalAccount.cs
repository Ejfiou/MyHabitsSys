using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyHabits.DataEntity;

namespace MyHabits.DataAccess
{
    public class DalAccount
    {
        public List<UserEntity> GetUserInfo(UserEntity query)
        {
            string sql = "select * from [userinfo] where 1=1";

            var p = new DynamicParameters();
            if (!string.IsNullOrEmpty(query.userName))
            {
                sql += " and UserName = @UserName ";
                p.Add("@UserName", query.userName);
            }
            if (!string.IsNullOrEmpty(query.password))
            {
                sql += " and password = @password ";
                p.Add("@password", query.password);
            }
            var user = DbHelper.Query<UserEntity>(sql,p);
            return user;
        }

        public bool UserRegist(UserEntity user)
        {
          
            var flag = DbHelper.Insert<UserEntity>(user);
            return flag>0;
        }
    }
}
