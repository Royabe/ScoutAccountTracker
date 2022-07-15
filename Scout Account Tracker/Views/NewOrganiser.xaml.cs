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

namespace Scout_Account_Tracker
{
    /// <summary>
    /// Interaction logic for NewOrganiser.xaml
    /// </summary>
    public partial class NewOrganiser : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options: new());
        public NewOrganiser()
        {
            InitializeComponent();
        }
        public async void BtnAddOrg_click(object sender, RoutedEventArgs e)
        {
            //Creates a new organiser
            await _context.Database.ExecuteSqlRawAsync($"Insert into dbo.organiser(Name) values('{OrgName.Text}');");
        }
        public void BtnReturn_click(object sender, RoutedEventArgs e)
        {
            EditOrganiser editOrg = new EditOrganiser();
            editOrg.Show();
            Close();
        }
    }
}
