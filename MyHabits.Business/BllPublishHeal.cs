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
        public bool HealthSet(PublishHeal heal)
        {
            return dal.HealthSet(heal);
        }
        public List<PublishHeal> SetHealthInfo(PublishHeal healthinfo)
        {
            return dal.SetHealthInfo(healthinfo);
        }
    }
}
