using MyHabits.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.DataAccess
{
  public  class DalQuestion
    {

        public bool SetQuesinfo(Question ques)
        {

            var flag = DbHelper.Insert<Question>(ques);
            return flag > 0;
        }

    }
}
