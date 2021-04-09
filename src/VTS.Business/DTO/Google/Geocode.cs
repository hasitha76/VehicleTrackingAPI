using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.Business.DTO.Google
{
    public class Geocode
    {
        public LocationResult[] results { get; set; }
        public string status { get; set; }
    }
}
