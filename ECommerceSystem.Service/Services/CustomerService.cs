using ECommerceSystem.Core.IServices;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Core.DTO;

namespace ECommerceSystem.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public CustomerService(ECommerceDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CustomerReadDTO> AddCustomersAsync(CustomerCreateDTO customerCreateDto)
        {
            var customer=_mapper.Map<Customers>(customerCreateDto);
            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CustomerReadDTO>(customer);
         
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer=await _dbContext.Customers.FindAsync(id);
            if (customer == null) return false;            
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<CustomerReadDTO>> GetAllCustomersAsync()
        {
            var customers= await _dbContext.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerReadDTO>>(customers);
                
        }

        public async Task<CustomerReadDTO> GetCustomersByIdAsync(int id)
        {
            var customers = await _dbContext.Customers.FindAsync(id);
            return _mapper.Map<CustomerReadDTO>(customers);
        }

        public async Task<bool> UpdateCustomersAsync(CustomerUpdateDTO customerUpdateDto,int id)
        {
            var existingcustomer = await _dbContext.Customers.FindAsync(id);
            if (existingcustomer == null) return false;
            _mapper.Map(customerUpdateDto,existingcustomer);
            _dbContext.Update(existingcustomer);
            
            return true;
        }
        public async Task<CustomerReadDTO> AuthenticateCustomerAsync(CustomerLoginDTO dto)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.EMail == dto.Email && c.Password == dto.Password);

            if (customer == null)
                return null;

            return _mapper.Map<CustomerReadDTO>(customer);
        }
    }
}
