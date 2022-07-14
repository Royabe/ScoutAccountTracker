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
    /// Interaction logic for EditOrganiser.xaml
    /// </summary>
    public partial class EditOrganiser : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options: new());
        IEnumerable<Organiser>? orglist;
        ObservableCollection<Billdata> billdata = new();
        public EditOrganiser()
        {
            InitializeComponent();
            DGOrg.ItemsSource = billdata;
        }
        public async void Window_Loaded(Object sender, EventArgs e)
        {
            //Puts the scout data into the table
            orglist = await _context.organiser.ToListAsync();
            foreach (Organiser i in orglist)
            {
                billdata.Add(new Billdata(i.Name, await sum(i) , await getBalance(i, DateTime.Now), await getBalance(i, new DateTime(DateTime.Now.Year, 3, 31))));
            }
        }
        //returns the total amount the scout has earned over all of the events.
        private async Task<float> getBalance(Organiser org, DateTime until)
        {
            float balance = 0;
            var bills = await _context.bills
                .Where(x => x.Date <= until)
                .Where(x => x.Org == org).ToListAsync();
            foreach (Bill i in bills)
            {
                balance += i.Value;
                var paym = await _context.payment
                    .Where(x => x.bill == i)
                    .Select(x => x.Value).ToArrayAsync();
                foreach (float j in paym) {
                    balance -= j;
                }
            }
            return balance;
        }
        private void btnAddOrg_click(object sender, RoutedEventArgs e)
        {
            NewOrganiser newOrg = new();
            newOrg.Show();
            Close();
        }
        private void btnViewOrg_click(object sender, RoutedEventArgs e)
        {
            ViewOrganiser vieworg = new ViewOrganiser(orglist.ToList()[DGOrg.SelectedIndex]);
            vieworg.Show();
            Close();
        }
        private void btnReturn_click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
        private async Task<float> sum(Organiser org)
        {
            float sum= 0;
            var paym = await _context.payment
                .Where(x=>x.bill!=null)
                .Where(x => x.bill.Org.ID == org.ID).ToListAsync();
            foreach (Payment i in paym)
            {
                sum += i.Value;
            }
            return sum;
        }
    }
    public class Billdata
    {
        public string Name { get; set; }
        public float Rev { get; set; }
        public float Bal { get; set; }
        public float Bal31 { get; set; }
        public Billdata(string name, float rev, float bal, float bal31)
        {
            Name = name;
            Rev = rev;
            Bal = bal;
            Bal31 = bal31;
        }
    }
}
