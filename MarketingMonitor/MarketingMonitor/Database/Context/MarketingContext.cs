using MarketingMonitor.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingMonitor.Database.Context
{
 public class MarketingContext : DbContext
  {
    public MarketingContext(string connectionString) : base(connectionString)
    {
    }

    public DbSet<Person> People { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<XmlRawData> XmlRawData { get; set; }
  }
}
