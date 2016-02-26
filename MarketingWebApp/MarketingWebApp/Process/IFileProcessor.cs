using MarketingWebApp.ViewModels;
using System.Collections.Generic;
using System.Web;

namespace MarketingWebApp.Process
{
  /// <summary>
  /// Interface for classes that know how to process files uploaded from the website.
  /// </summary>
  public interface IFileProcessor
  {
    IEnumerable<UploadFileResultVM> Upload(IEnumerable<HttpPostedFileBase> files);
  }
}