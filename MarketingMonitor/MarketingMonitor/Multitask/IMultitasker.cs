using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingMonitor.Multitask
{
  /// <summary>
  /// Interface for classes that know how to execute tasks on a thread.
  /// </summary>
  public interface IMultitasker : IFileTask
  {
    void Stop();
  }
}
