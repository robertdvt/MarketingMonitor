using System.Web;

namespace MarketingWebApp.Process
{
  /// <summary>
  /// Interface for classes that know how to persist marketing data that was uploaded via the website.
  /// </summary>
  public interface IMarketingDataPersistor
  {
    void Persist(HttpPostedFileBase file);
  }
}