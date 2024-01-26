using Countries.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Countries.Controllers
{
    /// <summary>
    /// Controller which calls API controller methodes
    /// Every known exception can have its own view....
    /// </summary>
    public class CountryController : Controller
    {
        public ActionResult Countries()
        {
            try
            {
                List<CountryModel> countries = GetCountries();
                return View(countries);
            }
            catch
            { return View("Error"); }
        }

        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch
            { return View("Error"); }

        }

        public ActionResult Details(int countryId)
        {
            try
            {
                var country = GetCountry(countryId);
                return View("Details", country);
            }
            catch
            { return View("Error"); }
        }

        public ActionResult Index()
        {
            try
            {
                List<CountryModel> countries = GetCountries();
                return View("Countries", countries);
            }
            catch
            { return View("Error"); }
        }

        private string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

        public ActionResult SaveCountry(CountryModel country)
        {
            try
            {
                string inputJson = (new JavaScriptSerializer()).Serialize(country);
                PostApi("AddCountry", inputJson);

                List<CountryModel> countries = GetCountries();
                return View("Countries", countries);
            }
            catch
            { return View("Error"); }
        }

        protected List<CountryModel> GetCountries()
        {
            return GetApi<List<CountryModel>>("GetCountries");            
        }

        protected CountryModel GetCountry(int countryid)
        {
            var input = new { CountryId = countryid };
            var inputJson = (new JavaScriptSerializer()).Serialize(input);

            return GetApi<CountryModel>("GetCountry", inputJson);            
        }

        private string CallApi(string name, string parameters="")
        {
            string apiUrl = Path.Combine(GetBaseUrl(), "api/CountryAPI");

            WebClient client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            return client.UploadString(Path.Combine(apiUrl, name), parameters);
        }

        private T GetApi<T>(string name, string parameters="")
        {
            string json = CallApi(name, parameters);
            T result = (new JavaScriptSerializer()).Deserialize<T>(json);
            return result;
        }

        private void PostApi(string name, string parameters = "")
        {
            string json = CallApi(name, parameters);            
        }
    }
}