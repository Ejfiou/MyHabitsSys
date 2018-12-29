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
    [Table("Emp")]
    [DataContract(IsReference = true)]
    [KnownType(typeof(UserEntity))]
    public class UserEntity
    {
        //eid, name, age, salary

        [Required]
        [DataMember]
        [Key]
        public int eid { get; set; }
        public string UserID { get; set; }

        public int age { get; set; }

        public string Password { get; set; }
    }
}
