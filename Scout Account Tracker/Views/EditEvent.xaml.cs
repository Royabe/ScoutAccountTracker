using Microsoft.EntityFrameworkCore;
using Scout_Account_Tracker.Models;
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

namespace Scout_Account_Tracker
{
    /// <summary>
    /// Interaction logic for EditEvent.xaml
    /// </summary>
    public partial class EditEvent : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options: new());
        IEnumerable<Event>? eventlist;
        ObservableCollection<Eventdata> events = new();
        public EditEvent()
        {
            InitializeComponent();
            DGEvent.ItemsSource=events;
        }
        public async void Window_Loaded(Object sender, EventArgs e)
        {
            //Puts the event data into the table
            eventlist = await _context.events.ToListAsync();
            foreach (Event i in eventlist)
            {
                events.Add(new Eventdata(i.Name,i.Location,i.Type,i.Start,i.End,i.Org.Name,await getAttn(i),await getHours(i),await getRev(i)));
            }
        }
        //Gets event attendance
        public async Task<int> getAttn(Event ev)
        {
            var et = await _context.eventsTime
                .Where(x=>x.SpEvent == ev).ToListAsync();
            return et.Count;
        }
        //Gets total hours worked for event
        public async Task<float> getHours(Event ev)
        {
            var et = await _context.eventsTime
                .Where(x => x.SpEvent == ev).ToListAsync();
            float hours = 0;
            foreach(EventTime i in et)
            {
                hours += (float)((i.End - i.Start).TotalHours);
            }
            return hours;
        }
        private void btnAddEvent_click(object sender, RoutedEventArgs e)
        {
            NewEvent newEvent = new NewEvent();
            newEvent.Show();
            Close();
        }
        private void btnViewEvent_click(object sender, RoutedEventArgs e)
        {
            ViewEvent viewEvent = new ViewEvent(eventlist.ToList()[DGEvent.SelectedIndex]);
            viewEvent.Show();
            Close();
        }
        private void btnReturn_click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
        //gets total revenue from event
        public async Task<float> getRev(Event ev)
        {
            var et = await _context.eventsTime
                .Where(x => x.SpEvent == ev).ToListAsync();
            var payRate = await _context.payRate
                .Where(x => x.Date.Year == ev.Start.Year).ToListAsync();
            float rev = 0;
            foreach (EventTime i in et)
            {
                float hours = (float)((i.End - i.Start).TotalHours);
                float rate = payRate.FirstOrDefault(x => x.Age == i.scout.getAge(i.Start)).Rate;
                rev += hours * rate;
            }
            return rev;
        }
    }
    public class Eventdata
    {
        public string Name { get; set; }
        public string Loc { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string org { get; set; }
        public int attn { get; set; }
        public float hours { get; set; }
        public float rev { get; set; }

        public Eventdata(string name, string loc, string type, DateTime start, DateTime end, string org, int attn, float hours, float rev)
        {
            Name = name;
            Loc = loc;
            Type = type;
            Start = start;
            End = end;
            this.org = org;
            this.attn = attn;
            this.hours = hours;
            this.rev = rev;
        }
    }
}
