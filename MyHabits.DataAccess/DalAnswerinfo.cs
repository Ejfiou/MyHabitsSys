using Dapper;
using MyHabits.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.DataAccess
{
    public class DalAnswerinfo
    {
        
        public bool SetAnswerinfo(Answerinfo answer)
        {

            var flag = DbHelper.Insert<Answerinfo>(answer);
            return flag > 0;
        }
        public bool GetAnswerinfo(Answerinfo answer)
        {
            string sql = "select * from Answerinfo where  1=1";
            var p = new DynamicParameters();
            sql += " and questionID = @questionID ";
            p.Add("@questionID", answer.questionID);
            sql += " and userID = @userID ";
            p.Add("@userID", answer.userID);
            var flag = DbHelper.Query<Answerinfo>(sql, p);
            return flag.Count> 0;
        }

        public List<Answerinfo> MyAnswerinfo(Answerinfo answer)
        {
            string sql = "select * from Answerinfo where  1=1";
            var p = new DynamicParameters();
            sql += " and questionID = @questionID ";
            p.Add("@questionID", answer.questionID);
            sql += " and userID = @userID ";
            p.Add("@userID", answer.userID);

            var MyAnswerinfo = DbHelper.Query<Answerinfo>(sql, p);

            return MyAnswerinfo;
        }
        
        public List<Answerinfo> AllAnswerinfo(Answerinfo answer)
        {
            string sql = "select * from Answerinfo where  1=1";
            var p = new DynamicParameters();
            sql += " and questionID = @questionID ";
            p.Add("@questionID", answer.questionID);

            var AllAnswerinfo = DbHelper.Query<Answerinfo>(sql, p);

            return AllAnswerinfo;
        }


    }
}
