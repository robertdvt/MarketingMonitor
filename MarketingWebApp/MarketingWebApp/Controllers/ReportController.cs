using MarketingWebApp.Process;
using MarketingWebApp.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarketingWebApp.Controllers
{
  public class ReportController : Controller
  {
    [Dependency]
    public IMarketingRepo MarketingRepo { get; set; }

    public ActionResult Index()
    {
      var people = MarketingRepo.GetPeople(); //TODO: Use a view model and not the database Person model

      return View(people);
    }

    public ActionResult Orders(int? id)
    {
      if (!id.HasValue)
      {
        return RedirectToAction("Index");
      }

      OrdersVM orders = new OrdersVM()
      {
        Person = MarketingRepo.GetPerson(id.Value),
        Orders = MarketingRepo.GetOrdersForPersonId(id.Value)
      };

      return View(orders);
    }
  }
}
