using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout_Account_Tracker.Models
{
    public class Scout
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("GroupID")]
        public virtual Group group { get; set; }
        [Required]
        public float Spent { get; set; } = 0;
        [Required]
        public DateTime DOB { get; set; } = DateTime.Now.AddYears(-14);
        public Scout(string name, float spent, DateTime dOB)
        {
            Name = name;
            Spent = spent;
            DOB = dOB;
        }
        public Scout(string name, Group group, float spent, DateTime dOB)
        {
            Name = name;
            this.group = group;
            Spent = spent;
            DOB = dOB;
        }
        public int getAge(DateTime Date)
        {
            int age = (Date.Year - DOB.Year);
            DateTime Thisyear = new DateTime(Date.Year, DOB.Month, DOB.Day);
            if (Date > Thisyear)
            {
                age -= 1;
            }
            return age;
        }
    }
}
