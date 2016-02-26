using log4net;
using MarketerMonitor.Database;
using MarketerMonitor.Report;
using MarketingMonitor;
using MarketingMonitor.Multitask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkertingServiceCA
{
  class MarketServiceCA
  {

    private static readonly ILog log = LogManager.GetLogger(typeof(MarketServiceCA));

    static void Main(string[] args)
    {
      log4net.Config.BasicConfigurator.Configure();
      log.Info("Start monitoring service...");

      if (args.Length < 3)
      {
        Console.WriteLine("Usage MarketingServiceCA [path-of-folder-to-monitor] [connectionString] [report-path-and-name]");
        return;
      }

      string xmlFolder = args[0];
      string connectionString = args[1];
      string reportPathAndName = args[2];

      IMultitasker dbWriter = new Multitasker(new DatabaseWriter(connectionString));
      IMultitasker reportWriter = new Multitasker(new ReportWriter(reportPathAndName));

      FolderMonitor monitor = new FolderMonitor(xmlFolder, dbWriter, reportWriter);

      log.Info("Monitoring: " + xmlFolder);
      log.Info("Database: "+connectionString);
      log.Info("Press enter to stop the monitoring service.");

      Console.ReadKey();

      dbWriter.Stop();
      reportWriter.Stop();
    }
  }
}
