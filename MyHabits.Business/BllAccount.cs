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
        public UserEntity GetUserInfo(object uid)
        {
            return dal.GetUserInfo(object uid);
        }
    }
}
