using MarketingMonitor.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingWebApp.ViewModels
{
  public class OrdersVM
  {
    public Person Person { get; set; }
    public IEnumerable<Order> Orders {get;set;}
  }
}
