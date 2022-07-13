using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Scout_Account_Tracker.Models
{
    public class PayRate
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public float Rate { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public PayRate(float rate, int age, DateTime date)
        {
            Rate = rate;
            Age = age;
            Date = date;
        }
    }
}
