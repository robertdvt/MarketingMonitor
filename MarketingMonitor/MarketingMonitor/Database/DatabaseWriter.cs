using log4net;
using MarketingMonitor.Database.Context;
using MarketingMonitor.Database.Models;
using MarketingMonitor.Multitask;
using MarketingMonitor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketerMonitor.Database
{
  public class DatabaseWriter : IFileTask
  {
    private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseWriter));

    private string connectionString;
    public DatabaseWriter(string connectionString)
    {
      this.connectionString = connectionString;
    }
    public void DoWork(string pathAndName)
    {
      try
      {
        log.Info("Database processing file: "+pathAndName);
        using (var context = new MarketingContext(connectionString))
        {
          XmlRawData xmlRawData = GetXmlRawRata(pathAndName);
          List<Person> people = GetPeople(pathAndName, xmlRawData);
          Persist(context, xmlRawData, people);
        }
        log.Info("Database finished with file: " + pathAndName);
      }
      catch (Exception e)
      {
        log.Error("Failed to process: " + pathAndName, e);
      }
    }
    private void Persist(MarketingContext context, XmlRawData xmlRawData, List<Person> people)
    {
      context.XmlRawData.Add(xmlRawData);

      foreach (Person person in people)
      {
        context.People.Add(person);
        context.Orders.AddRange(person.Orders);
      }
      context.SaveChanges();
    }

    private List<Person> GetPeople(string pathAndName, XmlRawData xmlRawData)
    {
      root data = MarketingDataProcessor.GetDataFromFile(pathAndName);

      List<Person> people = new List<Person>();
      foreach (rootPerson p in data.person)
      {
        Person person = GetPerson(p);
        person.XmlRawData = xmlRawData;
        people.Add(person);
      }
      return people;
    }

    private XmlRawData GetXmlRawRata(string pathAndName)
    {
      string rawXml = MarketingDataProcessor.GetRawXml(pathAndName);
      XmlRawData xmlRawData = new XmlRawData() { Xml = rawXml };
      return xmlRawData;
    }

    private Person GetPerson(rootPerson p)
    {

      Person person = new Person()
      {
        Email = p.email,
        Firstname = p.firstname,
        ReferenceNo = new Guid(p.referenceno),
        Surname = p.surname
      };

      List<Order> orders = GetOrders(p, person);
      person.Orders = orders;

      return person;
    }

    private List<Order> GetOrders(rootPerson p, Person person)
    {
      List<Order> orders = new List<Order>();
      foreach (rootPersonOrder o in p.orders)
      {
        Order order = new Order()
        {
          Person = person,
          OrderName = o.ordername,
          OrderReference = new Guid(o.orderreference),
          OrderValue = o.ordervalue
        };
        orders.Add(order);
      }
      return orders;
    }
  }
}
