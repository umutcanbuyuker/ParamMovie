using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set ; }
    }
}
