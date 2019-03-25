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
    }
}
