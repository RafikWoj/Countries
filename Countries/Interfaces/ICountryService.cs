using Countries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Interfaces
{
    public interface ICountryService<TDataModel> where TDataModel : CountryModel
    {
        Task<List<TDataModel>> GetCountries();

        Task<TDataModel> GetCountry(int id);

        Task<bool> SaveCountry(TDataModel country);
    }
}
