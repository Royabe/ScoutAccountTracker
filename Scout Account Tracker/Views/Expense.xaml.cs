using Scout_Account_Tracker.Models;
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

namespace Scout_Account_Tracker.Views
{
    /// <summary>
    /// Interaction logic for Expense.xaml
    /// </summary>
    public partial class Expense : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options: new());
        private Scout thisScout;
        public Expense(Scout scout)
        {
            thisScout = scout;
            InitializeComponent();
        }
        public async void BtnAddExp_click(object sender, RoutedEventArgs e)
        {
            //Inserts the new payment into the database
            DateTime now = DateTime.Now;
            await _context.Database.ExecuteSqlRawAsync($"Insert into dbo.payment(Name,Value,Date,ScoutID) values('{EName.Text}','{EVal.Text}','{now.Month}/{now.Day}/{now.Year}','{thisScout.ID}');");
        }
        public void BtnReturn_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
