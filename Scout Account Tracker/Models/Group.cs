using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scout_Account_Tracker.Models
{
    public class Group
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public Group(string name)
        {
            Name = name;
        }
    }

}
