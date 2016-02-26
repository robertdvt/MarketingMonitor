using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingMonitor.Database.Models;
using MarketingMonitor.Database.Context;
using System.Data.Entity;

namespace MarketingWebApp.Process
{
  public class MarketingRepo : IMarketingRepo
  {

    private string connectionString;
    public MarketingRepo(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public IEnumerable<Order> GetOrdersForPersonId(int idPerson)
    {
      using (var context = new MarketingContext(connectionString))
      {
        return context.Orders.Where(x=>x.Person.Id== idPerson).Include(path => path.Person).ToList();
      }
    }

    public IEnumerable<Person> GetPeople()
    {
      using (var context = new MarketingContext(connectionString))
      {
        return context.People.Include(path=>path.Orders).ToList();
      }
    }

    public Person GetPerson(int id)
    {
      using (var context = new MarketingContext(connectionString))
      {
        return context.People.Where(p => p.Id == id).FirstOrDefault();
      }
    }
  }
}
