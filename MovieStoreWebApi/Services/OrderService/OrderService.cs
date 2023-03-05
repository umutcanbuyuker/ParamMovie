using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebApi.DataAccess;
using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Order BuyMovie(OrderCreateDto orderCreateDto, IEnumerable<Claim> claimList)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == orderCreateDto.MovieId);

            var claimCustomerId = claimList.Where(x => x.Type == ClaimTypes.NameIdentifier).SingleOrDefault();
            
            var newOrder = new Order
            {
                MovieId = movie.Id,
                CustomerId=Convert.ToInt32(claimCustomerId.Value),
                OrderDate = orderCreateDto.OrderDate,
                Price = movie.Price
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            return newOrder;
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.SingleOrDefault(x => x.Id == orderId);

            if (order == null)
            {
                throw new InvalidOperationException("Sipariş bulunamadı");
            }

            order.IsActive = false;
            _context.SaveChanges();
        }

        public List<OrderListDto> GetAll()
        {
            var orders = _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Movie)
                .Where(x => x.IsActive != false)
                .OrderBy(x => x.Id);

            var orderLists = _mapper.Map<List<OrderListDto>>(orders);
            return orderLists;
        }

        public List<OrderListDto> GetAllOrderOfCustomer(int customerId)
        {
            var orders = _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Movie)
                .Where(x => x.CustomerId == customerId && x.IsActive != false)
                .OrderBy(x => x.Id);

            var orderLists = _mapper.Map<List<OrderListDto>>(orders);
            return orderLists;
        }
    }
}
