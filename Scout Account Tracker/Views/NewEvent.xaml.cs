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

namespace Scout_Account_Tracker
{
    /// <summary>
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class NewEvent : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options: new());
        public NewEvent()
        {
            InitializeComponent();
        }
        public async void BtnAddEvent_click(object sender, RoutedEventArgs e)
        {
            if (EName.Text != "" && ELoc.Text != "" && EDate.Text != "" && EStart.Text != "" && EEnd.Text != "" && EOrg.Text != "") 
            {
                DateTime start = DateTime.Parse(EStart.Text);
                DateTime end = DateTime.Parse(EEnd.Text);
                DateTime Date = DateTime.Parse(EDate.Text);
                DateTime Startdate = new DateTime(Date.Year,Date.Month,Date.Day,start.Hour,start.Minute,start.Second);
                DateTime Enddate = new DateTime(Date.Year, Date.Month, Date.Day, end.Hour, end.Minute, end.Second);
                Organiser org = await _context.organiser.FirstOrDefaultAsync(x => x.Name.ToLower() == EOrg.Text.ToLower());
                if (org != null)
                {
                    //await _context.Database.ExecuteSqlRawAsync(
                    //    $"Insert into dbo.events([Name],[Location],[Type],[Start],[End],[OrgID]) values('{EName.Text}','{ELoc.Text}','{EType.Text}','{Date.Month}/{Date.Day}/{Date.Year}','{Date.Month}/{Date.Day}/{Date.Year}','{org.ID}');");
                    _context.events.Add(new(EName.Text, ELoc.Text, EType.Text, Startdate, Enddate, org));
                    _context.SaveChanges();
                }
                BtnReturn_click(this, e);
            }
        }
        public void BtnReturn_click(object sender, RoutedEventArgs e)
        {
            EditEvent editEvent = new EditEvent();
            editEvent.Show();
            Close();
        }
    }
}
