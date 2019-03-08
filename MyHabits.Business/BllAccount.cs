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
    }
}
