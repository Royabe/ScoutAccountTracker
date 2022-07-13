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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Scout_Account_Tracker.Models;

namespace Scout_Account_Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options:new());
        public MainWindow()
        {
            InitializeComponent();
            
        }
        public void OnLoaded(object sender, RoutedEventArgs e)
        {
             _context.Database.EnsureCreated();
        }
        public void EditScout_Click(object sender, RoutedEventArgs e) 
        {
            EditScout win = new EditScout();
            Close();
            win.Show();
        }
        public void EditEvent_Click(object sender, RoutedEventArgs e) 
        {
            EditEvent win = new EditEvent();
            Close();
            win.Show();
        }
        public void EditOrg_Click(object sender, RoutedEventArgs e) 
        {
            EditOrganiser win = new EditOrganiser();
            Close();
            win.Show();
        }
    }
}
