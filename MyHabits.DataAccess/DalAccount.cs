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
        public List<UserEntity> GetUserInfo(UserEntity query,bool isLogin = false)
        {
            string sql = "select * from [userinfo] where 1=1";

            var p = new DynamicParameters();
            if (!string.IsNullOrEmpty(query.userName))
            {
                sql += " and UserName = @UserName ";
                p.Add("@UserName", query.userName);
            }

           

            if (isLogin)
            {
                sql += " and password = @password ";
                p.Add("@password", query.password);
            }
            else  if(!string.IsNullOrEmpty(query.password))
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

        public bool UpdateUser(UserEntity user)
        {
            return DbHelper.Update(user); 
        }


        public bool UpdatePwd(UserEntity user)
        {
            return DbHelper.Update(user);
        }
        public List<UserEntity> GetMyuserInfo(UserEntity user)
        {
            string sql = "select * from userinfo where  1=1";
            var p = new DynamicParameters();
            sql += " and ID = @ID ";
            p.Add("@ID", user.ID);

            var Myuserinfo = DbHelper.Query<UserEntity>(sql, p);

            return Myuserinfo;
        }
        public List<UserEntity> UserIMg(UserEntity user)
        {
            string sql = "select * from userinfo where  1=1";
            var p = new DynamicParameters();
            if (!string.IsNullOrEmpty(user.userName))
            {
                sql += " and UserName = @UserName ";
                p.Add("@UserName", user.userName);
            }
            var MyUserIMg = DbHelper.Query<UserEntity>(sql, p);
            return MyUserIMg;
        }
        
         public List<UserEntity> UserPsd(UserEntity user)
        {
            string sql = "select * from userinfo where  1=1";
            var p = new DynamicParameters();
            if (!string.IsNullOrEmpty(user.password))
            {
                sql += " and password = @password ";
                p.Add("@password", user.password);
                sql += " and ID = @ID ";
                p.Add("@ID", user.ID);
            }
            var MyUserPsd = DbHelper.Query<UserEntity>(sql, p);
            return MyUserPsd;
        }

        

        public List<UserEntity> GetPersonalList(out int total, int page, int limit)
        {
            string sql = "select * from userinfo ";
            var PersonalList = DbHelper.Query<UserEntity>(sql);
            total = PersonalList.Count;

            return PersonalList.Skip((page - 1) * limit).Take(limit).ToList();
        }




    }
}

