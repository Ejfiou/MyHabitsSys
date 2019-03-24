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
    [Table("Questioninfo")]
    [DataContract(IsReference = true)]
    [KnownType(typeof(Question))]
   public class Question
    {
        [DataMember]
        [Key]
        public int questionID { get; set; }

        [Required]
        [DataMember]
        public string question_title { get; set; }

        [Required]
        [DataMember]
        public string question_centent { get; set; }

        [Required]
        [DataMember]
        public int question__status { get; set; }

        [DataMember]
        public string question_prefix { get; set; }
    }
}
