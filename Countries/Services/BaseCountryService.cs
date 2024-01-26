using Countries.Interfaces;
using Countries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Countries.Services
{
    /// <summary>
    /// Base service, which operates on static list
    /// Commented in UnityConfig
    /// </summary>
    public class BaseCountryService : ICountryService<CountryModel>
    {
        public static List<CountryModel> Countries = new List<CountryModel>();
        public async Task<List<CountryModel>> GetCountries()
        {
            var countries = await getCountries();
            return countries;
        }

        public async Task<CountryModel> GetCountry(int id)
        {
            return await getCountry(id);
        }

        public async Task<bool> SaveCountry(CountryModel country)
        {
            try
            {
                await saveCountry(country);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private async Task<List<CountryModel>> getCountries()
        {
            
                if (!Countries.Any())
                {
                    CountryModel cm = new CountryModel();
                    cm.CountryId = 1;
                    cm.Name = "POlska";
                    cm.Capital = "Warszawa";
                    cm.Population = 42;
                    Countries.Add(cm);

                    cm = new CountryModel();
                    cm.CountryId = 2;
                    cm.Name = "Belgia";
                    cm.Capital = "Bruksela";
                    cm.Population = 3;
                    Countries.Add(cm);
                }
                return Countries;
        }

        private async Task<CountryModel> getCountry(int id)
        {
            var country = Countries.Where(c => c.CountryId == id).FirstOrDefault();
            if (country == null)
                country = new CountryModel() {CountryId=-1, Name = $"There's no country with id {id}!" };
            return country;            
        }

        private async Task saveCountry(CountryModel country)
        {
            country.CountryId = Countries.Max(x => x.CountryId) + 1;
            Countries.Add(country);
        }


    }
}