using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VTS.Business.DTO
{
    public class VehicleDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Make { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Model { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string VIN { get; set; }
    }
}
