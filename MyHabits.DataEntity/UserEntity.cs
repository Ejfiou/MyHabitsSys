using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyHabits.DataEntity
{
    [Table("user")]
    [DataContract(IsReference = true)]
    [KnownType(typeof(UserEntity))]
    public class UserEntity
    {
  
       
        [DataMember]
        [Key]
        public int ID { get; set; }

        [Required]
        [DataMember]
        public string userName { get; set; }

        [Required]
        [DataMember]
        public string password { get; set; }

        [DataMember]
        public int userAge { get; set; }

        [DataMember]
        public int userSex { get; set; }

        [DataMember]
        public string userImg { get; set; }

        [DataMember]
        public string userEmail { get; set; }

        [DataMember]
        public string userStatus { get; set; }

        [DataMember]
        public int userQQ { get; set; }

        [DataMember]
        public int userPhone { get; set; }
    }
}
