using log4net;
using MarketingWebApp.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MarketingWebApp.Process
{
  public class FileProcessor : IFileProcessor
  {
    private static readonly ILog log = LogManager.GetLogger(typeof(FileProcessor));

    private string xsdPathAndFile;

    [Dependency]
    public IMarketingDataPersistor Persistor { get; set; }

    public FileProcessor(string xsdPathAndFile)
    {
      this.xsdPathAndFile = xsdPathAndFile;
    }

    public IEnumerable<UploadFileResultVM> Upload(IEnumerable<HttpPostedFileBase> files)
    {
      List<UploadFileResultVM> results = new List<UploadFileResultVM>();

      foreach (HttpPostedFileBase file in files)
      {

        try
        {
          if (file.ContentLength > 0)
          {

            bool isValid = IsValid(file);

            UploadFileResultVM result = new UploadFileResultVM() { Filename = file.FileName, IsUploaded = false };

            if (isValid)
            {
              Persistor.Persist(file);
              result.IsUploaded = true;
            }

            results.Add(result);
          }
        }
        catch (Exception e)
        {
          log.Error("Failed to upload file.", e);
          UploadFileResultVM result = new UploadFileResultVM() { Filename = file.FileName, IsUploaded = false };
          results.Add(result);
        }
      }

      return results;
    }

    private bool IsValid(HttpPostedFileBase file)
    {
      XmlSchemaSet schemas = new XmlSchemaSet();
      schemas.Add(null, xsdPathAndFile);

      Exception firstException = null;

      var settings = new XmlReaderSettings
      {
        Schemas = schemas,
        ValidationType = ValidationType.Schema,
        ValidationFlags =
                             XmlSchemaValidationFlags.ProcessIdentityConstraints |
                             XmlSchemaValidationFlags.ReportValidationWarnings
      };
      settings.ValidationEventHandler +=
          delegate (object sender, ValidationEventArgs args)
          {
            if (args.Severity == XmlSeverityType.Warning)
            {
              log.Warn(args.Message);
            }
            else
            {
              if (firstException == null)
              {
                firstException = args.Exception;
              }
              log.Error("Deserialize failed", args.Exception);
            }
          };

      root obj = null;
      using (XmlReader reader = XmlReader.Create(file.InputStream, settings))
      {
        XmlSerializer ser = new XmlSerializer(typeof(root));
        obj = (root)ser.Deserialize(reader);
      }

      return firstException == null;

    }
  }
}
