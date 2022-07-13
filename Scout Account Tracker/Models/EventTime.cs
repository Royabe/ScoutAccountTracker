using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Scout_Account_Tracker.Models
{
    public class EventTime
    {

        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("ScoutID")]
        public virtual Scout scout { get; set; }
        [Required]
        [ForeignKey("EventID")]
        public virtual Event SpEvent { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public EventTime(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        public EventTime(Scout scout, Event spEvent, DateTime start, DateTime end)
        {
            this.scout = scout;
            SpEvent = spEvent;
            Start = start;
            End = end;
        }

    }
}
