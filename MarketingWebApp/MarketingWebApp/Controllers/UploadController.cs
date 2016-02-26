using MarketingWebApp.Process;
using MarketingWebApp.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketingWebApp.Controllers
{
  public class UploadController : Controller
  {
    [Dependency]
    public IFileProcessor FileUploader { get; set; }

    public UploadController()
    {
    }

    public ActionResult Upload()
    {
      ViewBag.Results = TempData["Results"] as IEnumerable<UploadFileResultVM>;
      return View("Upload");
    }

    [HttpPost]
    public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
    {
      IEnumerable<UploadFileResultVM> results = null;
      try
      {
        results = FileUploader.Upload(files);
      }
      catch (Exception)
      {
        results = null;
      }
      TempData["Results"] = results;
      return RedirectToAction(actionName: "Upload");
    }
  }
}
