using MyHabits.DataAccess;
using MyHabits.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.Business
{
     public class BllAnswerinfo
     {
        DalAnswerinfo dal = new DalAnswerinfo();

        public bool SetAnswerinfo(Answerinfo answer)
        {
            return dal.SetAnswerinfo(answer);
        }


        public List<Answerinfo> MyAnswerinfo(Answerinfo answer)
        {
            return dal.MyAnswerinfo(answer);
        }
        

        public List<Answerinfo> AllAnswerinfo(Answerinfo answer)
        {
            return dal.AllAnswerinfo(answer);
        }


    }
}
