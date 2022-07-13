using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout_Account_Tracker.Models
{
    public class Bill
    {

        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("OrgID")]
        public virtual Organiser Org { get; set; }
        [Required]
        public int Status { get; set; } = 0;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Value { get; set; }=0;
        public Bill( DateTime date, int value)
        {
            Date = date;
            Value = value;
        }
        public Bill(Organiser org, DateTime date, int value)
        {
            Org = org;
            Date = date;
            Value = value;
        }
    }
}
