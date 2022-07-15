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

namespace Scout_Account_Tracker.Views
{
    /// <summary>
    /// Interaction logic for Payments.xaml
    /// </summary>
    public partial class Payments : Window
    {
        private readonly ScoutDBContext _context = new(options: new());
        public ObservableCollection<Payment> PayRecords = new();
        IEnumerable<Payment> Paylist;
        Bill bill;
        public Payments(Bill thisBill)
        {
            bill= thisBill;
            OnLoaded();
            InitializeComponent();
            DGPay.ItemsSource = PayRecords;
        }
        public async void OnLoaded()
        {
            //Creates the observable for the datatable
            Paylist = await _context.payment
                .Where(x => x.bill.ID == bill.ID).ToListAsync();
            foreach (Payment i in Paylist)
            {
                PayRecords.Add(i);
            }
        }
        public async void AddPay_click(object sender, RoutedEventArgs e)
        {
            //Inserts the new payment into the database
            DateTime now = DateTime.Now;
            await _context.Database.ExecuteSqlRawAsync($"Insert into dbo.payment(Name,Value,Date,BillID) values('{PName.Text}','{PVal.Text}','{now.Month}/{now.Day}/{now.Year}','{bill.ID}');");
        }
        public void BtnDelBill_click(object sender, EventArgs e)
        {
            _context.Remove(bill);
            _context.SaveChanges();
            BtnReturn_click(this, e);
        }
        public void BtnDelete_click(object sender, EventArgs e)
        {
            _context.Remove(Paylist.ToList()[DGPay.SelectedIndex]);
            _context.SaveChanges();
            DGPay.ItemsSource=PayRecords;
        }
        public void BtnReturn_click(object sender, EventArgs e)
        {
            //changes bill status dependent upon payment amount
            float paytot = 0;
            foreach(Payment i in Paylist)
            {
                paytot += i.Value;
            }
            if (paytot == 0)
            {
                bill.Status = 0;
            }
            else if(paytot < bill.Value)
            {
                bill.Status = 1;
            }
            else
            {
                bill.Status = 2;
            }
            ViewOrganiser viewOrg = new(bill.Org);
            viewOrg.Show();
            Close();
        }
    }
}
