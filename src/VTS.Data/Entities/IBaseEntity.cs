using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.Data.Entities
{
    public interface IBaseEntity
    {
        Guid ID { get; set; }
    }
}
