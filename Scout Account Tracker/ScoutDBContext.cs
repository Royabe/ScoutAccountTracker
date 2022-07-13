using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scout_Account_Tracker.Models;

namespace Scout_Account_Tracker
{
    internal class ScoutDBContext : DbContext
    {
        public DbSet<Bill> bills { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<EventTime> eventsTime { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Organiser> organiser { get; set; }
        public DbSet<Payment> payment { get; set; }
        public DbSet<Scout> scouts { get; set; }
        public DbSet<PayRate> payRate { get; set; }
        public ScoutDBContext(DbContextOptions<ScoutDBContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ScoutDB");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
