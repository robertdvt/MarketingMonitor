using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace MarketingWebApp.Process
{
  public class MarketingDataPersistor : IMarketingDataPersistor
  {
    private string xmlFolder;

    public MarketingDataPersistor(string xmlFolder)
    {
      this.xmlFolder = xmlFolder;
    }

    /// <summary>
    /// TODO: Not sure of this method should get a HttpPostedFileBase or rather an object. The object would need to be serialized.
    /// TODO: I do not like the dependency on HttpPostedFileBase. Too tightly coupled to web app.
    /// </summary>
    /// <param name="file"></param>
    public void Persist(HttpPostedFileBase file)
    {
      var filename = Path.GetFileName(file.FileName);
      var path = Path.Combine(@"c:\temp", filename);
      file.SaveAs(path);
    }
  }
}
