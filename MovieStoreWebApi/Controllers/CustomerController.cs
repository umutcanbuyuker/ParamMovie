using Microsoft.AspNetCore.Mvc;
using MovieStoreWebApi.Entities.DTOs;
using MovieStoreWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_customerService.GetAll());
        }

        [HttpPost]
        public IActionResult Create(CustomerCreateDto customerCreateDto)
        {
            var customer = _customerService.CreateCustomer(customerCreateDto);
            return Ok(customer);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);
            return Ok();
        }
    }
}
