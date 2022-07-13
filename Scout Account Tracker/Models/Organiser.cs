using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Scout_Account_Tracker.Models
{
    public class Organiser
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public Organiser(string name)
        {
            Name = name;
        }
    }
}
