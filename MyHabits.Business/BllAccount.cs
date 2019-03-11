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
        public List<UserEntity> GetUserInfo(UserEntity query)
        {
            return dal.GetUserInfo(query);
        }

        public bool UserRegist(UserEntity user)
        {
            return dal.UserRegist(user);
        }

        public bool UpdateUserImg(UserEntity user)
        {
            return dal.UpdateUserImg(user);
        }

        public UserEntity GetUserById(int id)
        {
            return dal.GetUserById(id);
        }
    }
}
