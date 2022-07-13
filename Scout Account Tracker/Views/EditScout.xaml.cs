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
    /// Interaction logic for EditScout.xaml
    /// </summary>
    public partial class EditScout : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options: new());
        IEnumerable<Scout>? scoutlist;
        ObservableCollection<scoutdata> scouts = new();
        public EditScout()
        {
            InitializeComponent();
            DGscout.DataContext = scouts;
        }
        public async void Window_Loaded(Object sender, EventArgs e)
        {
            //Puts the scout data into the table
            scoutlist = await _context.scouts.ToListAsync();
            foreach (Scout i in scoutlist)
            {
                scouts.Add(new scoutdata(i.Name,i.group.Name,await getBalance(i, DateTime.Now) - i.Spent,await getBalance(i, new DateTime(DateTime.Now.Year, 3, 31)) - i.Spent));
            }
        }
        //returns the total amount the scout has earned over all of the events.
        private async Task<float> getBalance(Scout scout, DateTime until)
        {
            var events = await _context.eventsTime
                .Where(x => x.Start <= until)
                .Where(x => x.scout == scout).ToListAsync();
            float balance = 0;
            foreach (EventTime i in events)
            {
                var payrate = await _context.payRate
                    .Where(x => x.Date.Year == i.Start.Year)
                    .Where(x => x.Age == (int)(i.Start.Year - scout.DOB.Year))
                    .Select(x => x.Rate).ToArrayAsync();
                int duration = (i.End - i.Start).Seconds;
                balance += duration * payrate[0] / 3600;
            }
            return balance;
        }
        private void btnAddScout_click(object sender, RoutedEventArgs e)
        {
            NewScout newScout = new NewScout();
            newScout.Show();
            this.Close();
        }
    }
    public class scoutdata
    {
        public string Name;
        public string Group;
        public float Bal;
        public float Bal31;
        public scoutdata(string name, string group, float bal, float bal31)
        {
            Name = name;
            Group = group;
            Bal = bal;
            Bal31 = bal31;
        }
    }
}
