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
    /// Interaction logic for ViewOrganiser.xaml
    /// </summary>
    public partial class ViewOrganiser : Window
    {
        private readonly ScoutDBContext _context = new(options: new());
        public ObservableCollection<BillRecord> BillRecords = new();
        IEnumerable<Bill> bills;
        IEnumerable<Payment> paymlist;
        Organiser thisOrg;

        public ViewOrganiser(Organiser org)
        {
            thisOrg = org;
            OnLoaded();
            InitializeComponent();
            if (BillRecords != null)
            {
                BillRecords.OrderBy(x => x.Date);
            }
            DGBills.ItemsSource = BillRecords;
            OName.Text = org.Name;
        }
        public async void OnLoaded()
        {
            bills = await _context.bills
                .Where(x => x.Org.ID == thisOrg.ID).ToListAsync();
            paymlist = await _context.payment
                .Where(x => x.bill != null)
                .Where(x => x.bill.Org.ID == thisOrg.ID).ToListAsync();
            foreach (Bill i in bills)
            {
                BillRecords.Add(new BillRecord(DateOnly.FromDateTime(i.Date),i.Value,i.Status,TotalPaid(paymlist)));
            }
        }
        public float TotalPaid(IEnumerable<Payment> paymlist)
        {
            float val = 0;
            foreach(Payment paym in paymlist)
            {
                val += paym.Value;
            }
            return val;
        }
        public void BtnAddBill_click(object sender, EventArgs e)
        {
            Views.NewBill newbill = new(thisOrg);
            newbill.Show();
        }
        public void BtnPayments_click(object sender, EventArgs e)
        {
            Views.Payments payments = new(bills.ToList()[DGBills.SelectedIndex]);
            payments.Show();
            Close();
        }
        public void BtnDelete_click(object sender, EventArgs e)
        {
            _context.Remove(thisOrg);
            _context.SaveChanges();
            EditOrganiser editOrg = new();
            editOrg.Show();
            Close();
        }
        public void BtnDeleteBill_click(object sender, RoutedEventArgs e)
        {
            _context.Remove(bills.ToList()[DGBills.SelectedIndex]);
            _context.SaveChanges();
            DGBills.UpdateLayout();
        }
        public void BtnReturn_click(object sender, EventArgs e)
        {
            thisOrg.Name = OName.Text;
            _context.Update(thisOrg);
            _context.SaveChanges();
            EditOrganiser editOrg = new();
            editOrg.Show();
            Close();
        }
    }
    public class BillRecord
    {
        public DateOnly Date { get; set; }
        public float Value { get; set; }
        public int Status { get; set; }
        public float Paid { get; set; }

        public BillRecord(DateOnly date, float value, int status, float paid)
        {
            Date = date;
            Value = value;
            Status = status;
            Paid = paid;
        }
    }
}
