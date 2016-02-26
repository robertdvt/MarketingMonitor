using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingMonitor.Database.Models
{
  public class Person
  {

    public int Id { get; set; }

    public string Firstname { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public Guid ReferenceNo { get; set; }

    [Required]
    public virtual XmlRawData XmlRawData { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
  }
}
