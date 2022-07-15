using System;
using System.Collections.Generic;
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

namespace Scout_Account_Tracker.Views
{
    /// <summary>
    /// Interaction logic for NewBill.xaml
    /// </summary>
    public partial class NewBill : Window
    {
        private readonly ScoutDBContext _context = new(options: new());
        Organiser thisOrg;
        public NewBill(Organiser org)
        {
            thisOrg= org;
            InitializeComponent();
        }
        public async void BtnAddBill_click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Parse(BDate.Text);
            await _context.Database.ExecuteSqlRawAsync($"Insert into dbo.bills(OrgID,Status,Date,Value) values('{thisOrg.ID}','0','{date.Month}/{date.Day}/{date.Year}','{BVal.Text}');");
            Close();
        }
        public void BtnReturn_click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
