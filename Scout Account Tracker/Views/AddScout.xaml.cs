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
    /// Interaction logic for AddScout.xaml
    /// </summary>
    public partial class AddScout : Window
    {
        private readonly ScoutDBContext _context = new ScoutDBContext(options: new());
        public ObservableCollection<Scout> scouts = new();
        private IEnumerable<Scout> _scouts;
        private Event _event;
        public AddScout(Event ev)
        {
            InitializeComponent();
            _event = ev;
            DGscout.ItemsSource = scouts;
        }
        public async void OnLoaded(object sender, RoutedEventArgs e)
        {
            //Creates the observable for the datatable
            _scouts = await _context.scouts.ToListAsync();
            foreach(Scout i in _scouts)
            {
                scouts.Add(i);
            }
        }
        public void BtnReturn_click(object sender, RoutedEventArgs e)
        {
            DateTime start = DateTime.Parse(EStart.Text);
            DateTime end = DateTime.Parse(EEnd.Text);
            _context.eventsTime.Add(new(_scouts.ToList()[DGscout.SelectedIndex], _event, start, end));
            _context.SaveChanges();
        }

    }
}
