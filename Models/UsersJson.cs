using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zadatak1.Models
{

        public class UsersJson
        {
            public int id { get; set; }
            public string name { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public AddressJson address { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public CompanyJson company { get; set; }
        }
        public class GeoJson
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }

        public class AddressJson
        {
            public string street { get; set; }
            public string suite { get; set; }
            public string city { get; set; }
            public string zipcode { get; set; }
            public GeoJson geo { get; set; }
        }

        public class CompanyJson
        {
            public string name { get; set; }
            public string catchPhrase { get; set; }
            public string bs { get; set; }
        }

   
}