using Countries.Interfaces;
using Countries.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Countries.Services
{
    public class XmlCountryService : ICountryService<CountryModel>
    {
        public async Task<List<CountryModel>> GetCountries()
        {
            var countries = await readcountries();
            return countries;
        }

        private async Task<List<CountryModel>> readcountries()
        {
            var countries = new List<CountryModel>();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\Countries.xml");
            if (!File.Exists(path))
                savecountries(countries);
            
            string xml = File.ReadAllText(path);            
            try
            {
                 countries = ObjectSerializer.Deserialize<List<CountryModel>>(xml);
            }
            catch
            {   
                //should be logging
            }
            
            return countries;
        }

        private void savecountries(List<CountryModel> countries)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\Countries.xml");
            var c = ObjectSerializer.Serialize(countries);
            File.WriteAllText(path, c);
        }

        public async Task<CountryModel> GetCountry(int id)
        {
            var countries = await readcountries();
            var country = countries.Where(c => c.CountryId == id).FirstOrDefault();
            if (country == null)
                return new CountryModel() { CountryId = -1, Name = $"No country with id {id}." };
            return country;            
        }

        public async Task<bool> SaveCountry(CountryModel country)
        {
            try
            {
                var countries = await readcountries();
                country.CountryId = countries.Any() ? countries.Max(c => c.CountryId) + 1 : 1;
                countries.Add(country);
                savecountries(countries);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}