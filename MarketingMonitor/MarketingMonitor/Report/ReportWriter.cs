using log4net;
using MarketingMonitor.Multitask;
using MarketingMonitor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketerMonitor.Report
{
  public class ReportWriter : IFileTask
  {
    private static readonly ILog log = LogManager.GetLogger(typeof(ReportWriter));

    private string reportPathAndName;

    public ReportWriter(string reportPathAndName)
    {
      this.reportPathAndName = reportPathAndName;
    }

    public void DoWork(string pathAndName)
    {
      try
      {
        log.Info("Report processing file: " + pathAndName);

        string report = GetReport(pathAndName);
        WriteReport(report);

        log.Info("Report finished with file: " + pathAndName);
      }
      catch (Exception e)
      {
        log.Error("Failed to process: " + pathAndName, e);
      }
    }

    private void WriteReport(string report)
    {
      using (StreamWriter stream = new StreamWriter(reportPathAndName, true))
      {
        stream.WriteLine(report);
      }
    }

    private string GetReport(string pathAndName)
    {
      root data = MarketingDataProcessor.GetDataFromFile(pathAndName);

      StringBuilder sb = new StringBuilder();

      foreach (rootPerson p in data.person)
      {
        sb.AppendLine(String.Format("Person: firstname = {0} surname = {1} email = {2} referenceno = {3}", p.firstname, p.surname, p.email, p.referenceno));
      }
      return sb.ToString();
    }
  }
}
