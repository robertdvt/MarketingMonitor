//some change

using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using MarketingWebApp.Process;
using System.Configuration;

namespace MarketingWebApp
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
      string xmlFolder = ConfigurationManager.AppSettings["XMLFolder"];
      string xsdPathAndName = ConfigurationManager.AppSettings["XSDPathAndName"];
      string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

      container.RegisterType<IFileProcessor, FileProcessor>(new InjectionConstructor(xsdPathAndName));
      container.RegisterType<IMarketingDataPersistor, MarketingDataPersistor>(new InjectionConstructor(xmlFolder));
      container.RegisterType<IMarketingRepo, MarketingRepo>(new InjectionConstructor(connectionString));
    }
  }
}