using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieStoreWebApi.DataAccess;
using MovieStoreWebApi.Entities;
using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CustomerCreateDto CreateCustomer(CustomerCreateDto customerDto)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Email == customerDto.Email);

            if (customer != null)
            {
                throw new InvalidOperationException("Kullanıcı mevcut.");
            }

            var newCustomer = _mapper.Map<Customer>(customerDto);
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            foreach (var genre in customerDto.FavouriteGenres)
            {
                _context.CustomerGenres.Add(new CustomerGenre { CustomerId = newCustomer.Id, GenreId = genre });
                _context.SaveChanges();
            }

            return customerDto;
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
            {
                throw new InvalidOperationException("Kullanıcı bulunamadı.");
            }

            customer.IsActive = false;
            _context.SaveChanges();
        }

        public List<CustomerDto> GetAll()
        {
            var customers = _context.Customers.Include(x => x.FavouriteGenre).ThenInclude(x => x.Genre).Where(x => x.IsActive != false).OrderBy(x => x.Id);
            var customerList = new List<CustomerDto>();

            foreach (var item in customers)
            {
                customerList.Add(new CustomerDto
                {
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    FavouriteGenres = item.FavouriteGenre.Where(x => x.CustomerId == item.Id).Select(x => x.Genre.Name).ToList()
                });
            }
            return customerList;
        }
    }
}
