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
    [Table("Answerinfo")]
    [DataContract(IsReference = true)]
    [KnownType(typeof(Answerinfo))]
    public class Answerinfo
    {

        [DataMember]
        [Key]
        public int answerID { get; set; }

        [Required]
        [DataMember]
        public int questionID { get; set; }

        [Required]
        [DataMember]
        public int userID { get; set; }

        [Required]
        [DataMember]
        public string answer_centent { get; set; }

    }
}
