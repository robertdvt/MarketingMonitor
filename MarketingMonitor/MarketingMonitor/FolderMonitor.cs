using log4net;
using MarketingMonitor.Multitask;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MarketingMonitor
{
  public class FolderMonitor
  {
    private static readonly ILog log = LogManager.GetLogger(typeof(FolderMonitor));

    private FileSystemWatcher watcher;

    private IFileTask dbWriter;
    private IFileTask reportWriter;

    public FolderMonitor(string xmlFolder, IFileTask dbWriter, IFileTask reportWriter)
    {
      this.dbWriter = dbWriter;
      this.reportWriter = reportWriter;

      watcher = new FileSystemWatcher();
      watcher.Path = xmlFolder;
      watcher.NotifyFilter = NotifyFilters.LastWrite;
      watcher.Filter = "*.xml";
      watcher.Changed += Watcher_Changed;
      watcher.EnableRaisingEvents = true;
    }

    private void Watcher_Changed(object sender, FileSystemEventArgs e)
    {
      if (!IsFileLocked(e.FullPath))
      {
        log.Info("New file: " + e.FullPath);
        dbWriter.DoWork(e.FullPath);
        reportWriter.DoWork(e.FullPath);
      }
    }
    private bool IsFileLocked(string filename)
    {
      //TODO: This is not work correctly!
      try
      {
        using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
        {
          if (fileStream != null) { fileStream.Close(); }
        }
      }
      catch (IOException)
      {
        return true;
      }
      return false;
    }
  }
}
