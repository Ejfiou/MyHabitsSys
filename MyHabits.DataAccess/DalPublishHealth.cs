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
    }
}
