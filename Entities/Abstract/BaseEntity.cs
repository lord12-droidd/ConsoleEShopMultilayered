using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Rights { get; set; }
        
    }
}
