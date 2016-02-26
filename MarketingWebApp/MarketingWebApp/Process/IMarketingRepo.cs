using MarketingMonitor.Database.Models;
using System.Collections.Generic;

namespace MarketingWebApp.Process
{
  /// <summary>
  /// Repository for marketing data.
  /// </summary>
  public interface IMarketingRepo
  {
    IEnumerable<Person> GetPeople();
    Person GetPerson(int id);
    IEnumerable<Order> GetOrdersForPersonId(int idPerson);
  }
}