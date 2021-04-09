using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VTS.Business.DTO
{
    public class RecordingDTO
    {
        [Required]
        public Guid UserID { get; set; }
        [Required]
        public Guid DeviceID { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
    }
}
