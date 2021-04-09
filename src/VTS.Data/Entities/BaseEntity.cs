using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.Data.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
