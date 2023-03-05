using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.DTOs
{
    public class OrderListDto
    {
        public DateTime OrderDate { get; set; }
        public string Customer { get; set; }
        public string Movie { get; set; }
        public decimal Price { get; set; }
    }
}
