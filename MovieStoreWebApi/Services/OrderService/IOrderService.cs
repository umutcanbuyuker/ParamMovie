using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services.OrderService
{
    public interface IOrderService
    {
        public Order BuyMovie(OrderCreateDto orderCreateDto, IEnumerable<Claim> claimList);
        public List<OrderListDto> GetAll();
        public List<OrderListDto> GetAllOrderOfCustomer(int customerId);
        public void DeleteOrder(int orderId);
    }
}
