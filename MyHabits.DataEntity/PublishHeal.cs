using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.DataEntity
{
    [Table("healthinfo")]
    [DataContract(IsReference = true)]
    [KnownType(typeof(PublishHeal))]
    public class PublishHeal
    {

        [DataMember]    
        [Key]
        public int ID { get; set; }

        [Required]
        [DataMember]
        public string heal_title { get; set; }

        [Required]
        [DataMember]
        public string heal_content { get; set; }

        [DataMember]
        public int heal_typeID { get; set; }

        [Required]
        [DataMember]
        public DateTime heal_sdTime { get; set; }

        
        [DataMember]
        public int
           heal_count { get; set; }

        [Required]
        [DataMember]
        public string heal_status { get; set; }

        [DataMember]
        public string heal_img { get; set; }

    }
}

