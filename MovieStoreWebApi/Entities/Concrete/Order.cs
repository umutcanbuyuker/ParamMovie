using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.Concrete
{
    public class Order : BaseEntity
    {
        public bool IsActive { get; set; } = true;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
    }
}
