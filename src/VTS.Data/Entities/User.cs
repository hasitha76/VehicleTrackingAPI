using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.Data.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
