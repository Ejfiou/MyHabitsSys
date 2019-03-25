using MyHabits.DataAccess;
using MyHabits.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.Business
{
  public  class BllQuestion
    {
        DalQuestion dal = new DalQuestion();

        public bool SetQuesinfo(Question ques)
        {
            return dal.SetQuesinfo(ques);
        }

        public List<Question> GetQuesInfo(Question ques)
        {
            return dal.GetQuesInfo(ques);
        }
    }
}
