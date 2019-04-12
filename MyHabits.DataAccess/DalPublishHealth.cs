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
            string sql = "select top (7) ID,heal_title from healthinfo   where heal_typeID = 2 and heal_status =1 ORDER BY heal_sdTime DESC";

            var healleft = DbHelper.Query<PublishHeal>(sql);
            return healleft;
        }

        public bool UpdateHealthInfo2(PublishHeal heal)
        {

            var p = new DynamicParameters();
            p.Add("@ID", heal.ID);
            string sql = "update healthinfo set heal_status =1 where ID = @ID";


            var healFstatus = DbHelper.Execute(sql, p);

            return healFstatus > 0;
        }


        public List<PublishHeal> GetHealthInfo2(PublishHeal query)
        {
            string sql = "select heal_sdTime, ID,heal_title from healthinfo   where  heal_status =2 ORDER BY heal_sdTime DESC";

            var healleft2 = DbHelper.Query<PublishHeal>(sql);
            return healleft2;
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


        
        public List<PublishHeal> GetHealList(PublishHeal heal,out int total,int pageSize,int page)
        {
            string sql = "select heal_sdTime, ID,heal_title from healthinfo   where  heal_status =1";
            var p = new DynamicParameters();
            sql += " and heal_typeID = @heal_typeID ";
            p.Add("@heal_typeID", heal.heal_typeID);
            sql += "ORDER BY heal_sdTime DESC";
            var HealList = DbHelper.Query<PublishHeal>(sql,p);
            total = HealList.Count;
            
            return HealList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }


        public List<PublishHeal> GetHealNotop(PublishHeal healno)
        {
            string sql = "select top (10) ID,heal_title,heal_count from healthinfo where heal_status =1 ORDER BY heal_count DESC,heal_sdTime DESC";

            var healnotop = DbHelper.Query<PublishHeal>(sql);
            return healnotop;
        }


        public List<PublishHeal> GetHealRotation(PublishHeal query)
        {
            string sql = "select top (5) ID,heal_title from healthinfo where heal_status =1 ORDER BY heal_sdTime DESC";

            var HealRotation = DbHelper.Query<PublishHeal>(sql);
            return HealRotation;
        }




    }
}
