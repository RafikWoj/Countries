using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countries.Models
{
    public class CountryModel
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }

        public int Population { get; set; }
    }
}