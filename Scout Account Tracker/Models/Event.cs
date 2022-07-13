using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scout_Account_Tracker.Models
{
    public class Event
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public string Type { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        [ForeignKey("OrgID")]
        public virtual Organiser Org { get; set; }
        public Event(string name, string location, string type, DateTime start, DateTime end)
        {
            Name = name;
            Location = location;
            Type = type;
            Start = start;
            End = end;
        }
        public Event(string name, string location, string type, DateTime start, DateTime end, Organiser org)
        {
            Name = name;
            Location = location;
            Type = type;
            Start = start;
            End = end;
            Org = org;
        }
    }
}
