using MovieStoreWebApi.Entities;
using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services
{
    public interface ICustomerService
    {
        public CustomerCreateDto CreateCustomer(CustomerCreateDto customerDto);
        public List<CustomerDto> GetAll();
        public void DeleteCustomer(int id);
    }
}
