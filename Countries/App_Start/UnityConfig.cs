//using Countries.Interfaces;
//using Countries.Models;
//using Countries.Services;
//using System.Web.Http;
//using System.Web.Mvc;
//using Unity;
//using Unity.Mvc5;
using Countries.Interfaces;
using Countries.Logger;
using Countries.Models;
using Countries.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;


namespace Countries
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            //container.RegisterType<ICountryService<CountryModel>, BaseCountryService>();
            container.RegisterType<ICountryService<CountryModel>, XmlCountryService>();
            container.RegisterType<ILoggingService, LoggingService>();

            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}