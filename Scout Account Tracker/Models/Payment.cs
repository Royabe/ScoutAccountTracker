using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout_Account_Tracker.Models
{
    public class Payment
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("BillID")]
        public virtual Bill? bill { get; set; }
        [ForeignKey("ScoutID")]
        public virtual Scout? scout { get; set; }
        public Payment(int value, DateTime date)
        {
            Value = value;
            Date = date;
        }
        public Payment(int value, DateTime date, Bill? bill, Scout? scout)
        {
            Value = value;
            Date = date;
            this.bill = bill;
            this.scout = scout;
        }
    }
}
