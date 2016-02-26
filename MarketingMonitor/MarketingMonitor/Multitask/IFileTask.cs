using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingMonitor.Multitask
{
  /// <summary>
  /// Interface for tasks that the Multitasker can execute.
  /// </summary>
  public interface IFileTask
  {
    void DoWork(string pathAndName);

  }
}
