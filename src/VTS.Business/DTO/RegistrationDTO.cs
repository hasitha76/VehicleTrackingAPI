using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTS.Business.DTO
{
    public class RegistrationDTO
    {
        public  UserDTO User { get; set; }
        public  VehicleDTO Vehicle { get; set; }
        public  DeviceDTO Device { get; set; }
    }
}
