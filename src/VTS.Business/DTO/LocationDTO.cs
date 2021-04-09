using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.Business.DTO
{
    public class LocationDTO
    {
        public Guid DeviceID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime RecordedTime { get; set; }
        public string Location { get; set; }
    }
}
