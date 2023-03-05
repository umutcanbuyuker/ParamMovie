using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Entities.DTOs;
using MovieStoreWebApi.Services.OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("GetAllOrderOfCustomer")]
        public IActionResult GetAllOrderOfCustomer(int customerId)
        {
            return Ok(_orderService.GetAllOrderOfCustomer(customerId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderCreateDto orderCreateDto)
        {
            var claims = HttpContext.User.Claims;
            var order = _orderService.BuyMovie(orderCreateDto, claims);
            return Ok(order);
        }

        [HttpDelete]
        public IActionResult Delete(int orderId)
        {
            _orderService.DeleteOrder(orderId);
            return Ok();
        }
    }
}
