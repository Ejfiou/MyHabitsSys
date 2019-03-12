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

        public UserEntity GetUserById(int id)
        {
            string sql = "select * from [userinfo] where ID = @id";
            var p = new DynamicParameters();
            p.Add("@id", id);
            var user = DbHelper.Query<UserEntity>(sql, p);

            return user.Count>0 ? user[0]:null;
        }

        public bool UserRegist(UserEntity user)
        {
          
            var flag = DbHelper.Insert<UserEntity>(user);
            return flag>0;
        }

        public bool UpdateUserImg(UserEntity user)
        {
            return DbHelper.Update(user);
        }

        public List<UserEntity> SetMyuserInfo(UserEntity user)
        {
            string sql = "select * from userinfo where  1=1";
            var p = new DynamicParameters();
            sql += " and ID = @ID ";
            p.Add("@ID", user.ID);

            var Myuserinfo = DbHelper.Query<UserEntity>(sql, p);

            return Myuserinfo;
        }
    }
}

