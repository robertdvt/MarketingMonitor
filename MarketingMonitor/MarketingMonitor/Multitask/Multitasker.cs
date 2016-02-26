using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketingMonitor.Multitask
{
  /// <summary>
  /// Starts a new thread.
  /// Waits for incoming tasks.
  /// Tasks are scheduled on a concurrent dictonary.
  /// 
  /// TODO: Investigate alternatives for the concurrent dictionary.
  /// </summary>
  public class Multitasker : IMultitasker
  {
    private static readonly ILog log = LogManager.GetLogger(typeof(Multitasker));

    private IFileTask task;

    private ConcurrentDictionary<string,bool> files;

    private bool isRunning;

    private object lockObj = new object();

    public Multitasker(IFileTask task)
    {
      this.task = task;
      files = new ConcurrentDictionary<string,bool>();
      isRunning = true;

      new Thread(() =>
      {
        DoWork();
      }).Start();
    }

    public void DoWork(string pathAndName)
    {
      lock (lockObj)
      {
        if (!files.ContainsKey(pathAndName)) //avoid duplicates
        {
          log.Info("Adding: "+pathAndName);
          files.TryAdd(pathAndName,true);
        }
      }
     
    }

    public void Stop()
    {
      lock (lockObj)
      {
        isRunning = false;
      }
    }

    private void DoWork()
    {
      while (isRunning)
      {
       
        if (!files.IsEmpty)
        {
          log.Info("Processing...");

          KeyValuePair<string, bool>? pair =null;
          bool isHaveFilename = false;

          lock (lockObj)
          {
            pair = files.First();
            bool tmp;
            isHaveFilename = files.TryRemove(pair.Value.Key, out tmp);
          }

          if (isHaveFilename && pair.HasValue && !String.IsNullOrEmpty(pair.Value.Key))
          {
            string pathAndName = pair.Value.Key;
            log.Info("Processing: " + pathAndName);

            task.DoWork(pathAndName);
          }
        }
        Thread.Sleep(1000);
      }
    }
  }
}
