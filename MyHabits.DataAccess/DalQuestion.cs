using Dapper;
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


        public List<Question> GetQuesInfo(Question ques)
        {
            string sql = "select * from Questioninfo where  1=1";
            var p = new DynamicParameters();
            sql += " and questionID = @questionID ";
            p.Add("@questionID", ques.questionID);

            var Myquesinfo = DbHelper.Query<Question>(sql, p);

            return Myquesinfo;
        }

        public List<Question> GetAllQuesInfo(Question ques)
        {
            string sql = "select * from Questioninfo where question__status =1";

            var Myallquesinfo = DbHelper.Query<Question>(sql);

            return Myallquesinfo;
        }

        public bool UpdateFstatus(Question ques)
        {
            var p = new DynamicParameters();
            p.Add("@questionID", ques.questionID);
            string sql = "update Questioninfo set question__status =0 where questionID = @questionID";
     

            var quesFstatus = DbHelper.Execute(sql, p);

            return quesFstatus>0;
        }

    }
}
