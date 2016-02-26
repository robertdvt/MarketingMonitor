using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingMonitor.Database.Models
{
  public class Order
  {

    public int Id { get; set; }

    public Guid OrderReference { get; set; }

    public string OrderName { get; set; }

    public decimal OrderValue { get; set; }

    public virtual Person Person { get; set; }
  }
}
