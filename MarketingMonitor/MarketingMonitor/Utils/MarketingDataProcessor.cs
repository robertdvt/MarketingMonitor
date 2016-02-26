using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MarketingMonitor.Utils
{
  public static class MarketingDataProcessor
  {
    private static readonly ILog log = LogManager.GetLogger(typeof(MarketingDataProcessor));

    /// <summary>
    /// Deserialize Xml file to root object.
    /// </summary>
    /// <param name="pathAndName"></param>
    /// <returns></returns>
    public static root GetDataFromFile(string pathAndName)
    {
      root data = null;

      try
      {
        XmlSerializer serializer = new XmlSerializer(typeof(root));

        using (StreamReader reader = new StreamReader(pathAndName))
        {
          data = (root)serializer.Deserialize(reader);
        }
      }
      catch (Exception e)
      {
        log.Error("Failed to deserialize: " + pathAndName, e);
        data = null;
      }
      return data;
    }

    /// <summary>
    /// Return the raw xml from the file.
    /// </summary>
    /// <param name="pathAndName"></param>
    /// <returns></returns>
    public static string GetRawXml(string pathAndName)
    {
      return File.ReadAllText(pathAndName);
    }
  }
}
