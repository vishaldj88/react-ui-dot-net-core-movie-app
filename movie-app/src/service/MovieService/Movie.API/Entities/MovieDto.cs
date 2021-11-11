using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Api.Entities
{
    public class MovieDto
    {
        public int id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public bool status { get; set; }
    }


   
}
