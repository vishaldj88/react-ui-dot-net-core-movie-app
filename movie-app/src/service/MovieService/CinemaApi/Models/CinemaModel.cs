using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi
{
    public class CinemaModel
    {
      
        public int Id { get; set; }

        public string Name { get; set; }

        public string Director { get; set; }

        public string Release { get; set; }

        public string Producer { get; set; }

        public bool Hit { get; set; } = false;
    }

}
