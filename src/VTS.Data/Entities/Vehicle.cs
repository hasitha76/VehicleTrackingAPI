using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.Data.Entities
{
    public class Vehicle : BaseEntity
    {
        [MaxLength(30)]
        public string Make { get; set; }
        [MaxLength(30)]
        public string Model { get; set; }
        [MaxLength(30)]
        public string VIN { get; set; }
        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        public virtual Device Device { get; set; }
    }
}
