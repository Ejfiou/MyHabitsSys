using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHabits.DataAccess;
using MyHabits.DataEntity;
namespace MyHabits.Business
{
    public class BllAccount
    {
        DalAccount dal = new DalAccount();
        public List<UserEntity> GetUserInfo(UserEntity query,bool isLogin = false)
        {
            return dal.GetUserInfo(query,isLogin);
        }

        public bool UserRegist(UserEntity user)
        {
            return dal.UserRegist(user);
        }

        public bool UpdateUser(UserEntity user)
        {
            return dal.UpdateUser(user);
        }

        public bool UpdatePwd(UserEntity user)
        {
            return dal.UpdatePwd(user);
        }

        public UserEntity GetUserById(int id)
        {
            return dal.GetUserById(id);
        }
        public List<UserEntity> GetMyuserInfo(UserEntity user)
        {
            return dal.GetMyuserInfo(user);
        }
        public List<UserEntity> UserIMg(UserEntity user)
        {
            return dal.UserIMg(user);
        }

        public List<UserEntity> UserPsd(UserEntity user)
        {
            return dal.UserPsd(user);
        }

         public List<UserEntity> GetPersonalList(out int total,int page,int limit)
        {
            return dal.GetPersonalList(out total, page, limit);
        }
    }
}
