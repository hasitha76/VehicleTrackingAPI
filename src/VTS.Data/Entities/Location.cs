using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTS.Data.Entities
{
    public class Location : BaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Longitude { get; set; }
        
        [ForeignKey("Device")]
        public Guid DeviceID { get; set; }
        public virtual Device Device { get; set; }
    }
}
