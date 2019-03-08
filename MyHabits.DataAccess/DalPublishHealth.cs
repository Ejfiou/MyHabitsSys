using Dapper;
using MyHabits.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.DataAccess
{
    public class DalPublishHealth
    {
        public bool HealthSet(PublishHeal heal)
        {

            var flag = DbHelper.Insert<PublishHeal>(heal);
            return flag > 0;
        }

        public List<PublishHeal> GetHealthInfo(PublishHeal query)
        {
            string sql = "select top (7) ID,heal_title from healthinfo   where heal_typeID = '2' ORDER BY heal_sdTime DESC";

            var healleft = DbHelper.Query<PublishHeal>(sql);
            return healleft;
        }
        public List<PublishHeal> SetHealthInfo(PublishHeal query)
        {
            string sql = "select * from healthinfo where  1=1";
            var p = new DynamicParameters();
            sql += " and ID = @ID ";
            p.Add("@ID", query.ID);


            string sql1 = "update healthinfo set heal_count = heal_count+1 where id = @ID";
            DbHelper.Execute(sql1, p);

            var healthinfo = DbHelper.Query<PublishHeal>(sql, p);

            return healthinfo;
        }
    }
}
