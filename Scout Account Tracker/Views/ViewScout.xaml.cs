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
using Scout_Account_Tracker.Views;

namespace Scout_Account_Tracker
{
    /// <summary>
    /// Interaction logic for ViewScout.xaml
    /// </summary>
    public partial class ViewScout : Window
    {
        private readonly ScoutDBContext _context = new(options:new());
        public ObservableCollection<paymentrecord> ScoutRecords = new();
        IEnumerable<EventTime> eventlist;
        IEnumerable<Payment> paymlist;
        IEnumerable<PayRate> payRates;
        Scout thisScout;
        
        public ViewScout(Scout scout)
        {
            thisScout = scout;
            OnLoaded();
            InitializeComponent();
            if (ScoutRecords != null)
            {
                ScoutRecords.OrderBy(x => x.Date);
            }
            DGevents.ItemsSource = ScoutRecords;
            SName.Text = scout.Name;
            SGroup.Text = scout.group.Name;
            SDOB.Text = scout.DOB.ToShortDateString();
        }
        public async void OnLoaded()
        {
            eventlist = await _context.eventsTime
                .Where(x=>x.scout.ID == thisScout.ID).ToListAsync();
            paymlist = await _context.payment
                .Where(x => x.scout != null)
                .Where(x => x.scout.ID == thisScout.ID).ToListAsync();
            payRates = await _context.payRate.ToListAsync();
            foreach(EventTime i in eventlist)
            {
                ScoutRecords.Add(new paymentrecord(i.SpEvent.Name, i.Start, i.End, DateOnly.FromDateTime(i.Start), calcRevenue(i)));
            }
            foreach (Payment i in paymlist)
            {
                ScoutRecords.Add(new paymentrecord(i.Name, null, null, DateOnly.FromDateTime(i.Date), -1*i.Value));
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
                return (float)Duration*payRate.Rate;
            }
            else
            {
                return 0;
            }
            
        }
        public void BtnExpense_click(object sender, EventArgs e) 
        {
            Expense expense = new(thisScout);
            expense.Show();
        }
        public void BtnDelete_click(object sender, EventArgs e) 
        {
            _context.Remove(thisScout);
            _context.SaveChanges();
            EditScout editScout = new();
            editScout.Show();
            Close();
        }
        public void BtnReturn_click(object sender, EventArgs e) 
        {
            thisScout.Name = SName.Text;
            thisScout.group = _context.groups.FirstOrDefault(x=> x.Name == SGroup.Text);
            thisScout.DOB = DateTime.Parse(SDOB.Text);
            int spent = 0;
            foreach(Payment i in paymlist)
            {
                spent += i.Value;
            }
            thisScout.Spent = spent;
            _context.Update(thisScout);
            _context.SaveChanges();
            EditScout editScout = new();
            editScout.Show();
            Close();
        }
    }
    public class paymentrecord
    {
        public string Name { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public DateOnly Date { get; set; }
        public float Earned { get; set; }

        public paymentrecord(string name, DateTime? start, DateTime? end, DateOnly date, float earned)
        {
            Name = name;
            Start = start;
            End = end;
            Date = date;
            Earned = earned;
        }
    }
}
