using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Scout_Account_Tracker.Models;

namespace Scout_Account_Tracker
{
    /// <summary>
    /// Interaction logic for ViewEvent.xaml
    /// </summary>
    public partial class ViewEvent : Window
    {
        private readonly ScoutDBContext _context = new(options: new());
        public ObservableCollection<Eventrecord> EventRecords { get; set; }
        IEnumerable<EventTime> eventlist;
        IEnumerable<Payment> paymlist;
        IEnumerable<PayRate> payRates;
        Event thisEvent;
        public ViewEvent(Event @event)
        {
            thisEvent = @event;
            OnLoaded();
            InitializeComponent();
            DGevents.ItemsSource = EventRecords;
            EName.Text = @event.Name;
            ELoc.Text = @event.Location;
            EType.Text = @event.Type;
            EStart.Text = @event.Start.ToString();
            EEnd.Text = @event.End.ToString();
            EOrg.Text = @event.Org.Name;
        }
        public async void OnLoaded()
        {
            eventlist = await _context.eventsTime
                .Where(x => x.SpEvent.ID == thisEvent.ID).ToListAsync();
            foreach (EventTime i in eventlist)
            {
                EventRecords.Add(new Eventrecord(i.scout.Name, i.Start, i.End, calcRevenue(i)));
            }
        }
        public float calcRevenue(EventTime et)
        {
            double Duration = (et.End - et.Start).TotalHours;
            PayRate? payRate = payRates
                .Where(x => x.Date.Year == et.Start.Year)
                .FirstOrDefault(x => x.Age == et.scout.getAge(et.Start));
            if (payRate != null)
            {
                return (float)Duration * payRate.Rate;
            }
            else
            {
                return 0;
            }

        }
        public void BtnAddScout_click(object sender, EventArgs e)
        {
            
        }
        public void BtnDelete_click(object sender, EventArgs e)
        {
            _context.Remove(thisEvent);
            _context.SaveChanges();
            EditEvent editEvent = new();
            editEvent.Show();
            Close();
        }
        public void BtnReturn_click(object sender, EventArgs e)
        {
            thisEvent.Name = EName.Text;
            thisEvent.Location = ELoc.Text;
            thisEvent.Type = EType.Text;
            thisEvent.Start = DateTime.Parse(EStart.Text);
            thisEvent.End = DateTime.Parse(EEnd.Text);
            thisEvent.Org = _context.organiser.FirstOrDefault(x => x.Name == EOrg.Text);
            _context.Update(thisEvent);
            _context.SaveChanges();
            EditEvent editEvent = new();
            editEvent.Show();
            Close();
        }
    }
    public class Eventrecord
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public float Earned { get; set; }

        public Eventrecord(string name, DateTime start, DateTime end, float earned)
        {
            Name = name;
            Start = start;
            End = end;
            Earned = earned;
        }
    }
}
