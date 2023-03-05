using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Entities.DTOs
{
    public class OrderCreateDto
    {
        //public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
