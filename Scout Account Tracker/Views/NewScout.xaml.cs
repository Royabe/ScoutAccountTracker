using Microsoft.EntityFrameworkCore;
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
using Scout_Account_Tracker.Models;
using System.Data.SqlClient;

namespace Scout_Account_Tracker
{
    
    /// <summary>
    /// Interaction logic for NewScout.xaml
    /// </summary>
    public partial class NewScout : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options: new());
        public NewScout()
        {
            InitializeComponent();
        }
        public async void BtnAddScout_click(object sender, RoutedEventArgs e)
        {
            if (ScoutDob.Text == "")
            {
                ScoutDob.Text = new DateTime(DateTime.Now.Year - 14, 1, 1).ToString();
            }
            DateTime dob = DateTime.Parse(ScoutDob.Text);
            var group = _context.groups.Where(x => x.Name.ToLower() == ScoutGroup.Text.ToLower()).ToArray();
            if (group.Length == 0)
            {
                await _context.Database.ExecuteSqlRawAsync($"Insert into dbo.groups(Name) values('{ScoutGroup.Text}');");
                group = _context.groups.Where(x => x.Name.ToLower() == ScoutGroup.Text.ToLower()).ToArray();
            }
            await _context.Database.ExecuteSqlRawAsync($"Insert into dbo.scouts(Name,groupID,Spent,DOB) values('{ScoutName.Text}','{group[0].ID}','0','{dob.Month}/{dob.Day}/{dob.Year}');");
            BtnReturn_click(this,e);
        }
        public void BtnReturn_click(object sender, RoutedEventArgs e)
        {
            EditScout editScout = new EditScout();
            editScout.Show();
            this.Close();
        }
    }
}
