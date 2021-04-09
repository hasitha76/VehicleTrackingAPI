using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VTS.Business.DTO
{
    public class DeviceDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
