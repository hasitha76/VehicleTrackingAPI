using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.Data.Entities
{
    public class Device : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }

        [ForeignKey("Vehicle")]
        public Guid VehicleID { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
