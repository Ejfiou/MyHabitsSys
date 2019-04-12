using MyHabits.DataAccess;
using MyHabits.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.Business
{
   public class BllPublishHeal
    {
        DalPublishHealth dal = new DalPublishHealth();
        public List<PublishHeal> GetHealthInfo(PublishHeal query)
        {
            return dal.GetHealthInfo(query);
        }

        public List<PublishHeal> GetHealthInfo2(PublishHeal query)
        {
            return dal.GetHealthInfo2(query);
        }
        
        public bool UpdateHealthInfo2(PublishHeal heal)
        {
            return dal.UpdateHealthInfo2(heal);
        }
        public bool HealthSet(PublishHeal heal)
        {
            return dal.HealthSet(heal);
        }
        public List<PublishHeal> SetHealthInfo(PublishHeal healthinfo)
        {
            return dal.SetHealthInfo(healthinfo);
        }

        //数据库资讯列表
         public List<PublishHeal> GetHealList(PublishHeal heal, out int total, int pageSize, int page)
        {
            return dal.GetHealList(heal,out total,pageSize,page);
        }

        public List<PublishHeal> GetHealNotop(PublishHeal healno)
        {
            return dal.GetHealNotop(healno);
        }


        public List<PublishHeal> GetHealRotation(PublishHeal query)
        {
            return dal.GetHealRotation(query);
        }
        


    }
}
