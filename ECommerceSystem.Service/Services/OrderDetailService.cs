using AutoMapper;
using ECommerceSystem.Core.DTO;
using ECommerceSystem.Core.IServices;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Data.Repositories;
using ECommerceSystem.Service.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Service.Services
{
    public class OrderDetailService : IOrderDetailsService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderDetailService(ECommerceDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<OrderDetailReadDTO> AddOrderDetailsAsync(OrderDetailCreateDTO orderDetailCreateDto)
        {
            var orderdetails=_mapper.Map<OrderDetails>(orderDetailCreateDto);
            await _dbContext.AddAsync(orderdetails);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<OrderDetailReadDTO>(orderdetails);
           
        }

        public async Task<bool> DeleteOrderDetailsAsync(int id)
        {
            var orderdetails=await _dbContext.OrderDetails.FindAsync(id);
            if(orderdetails == null) return false;            
            _dbContext.OrderDetails.Remove(orderdetails);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDetailReadDTO>> GetAllOrdersAsync()
        {
            var orderdetail= await _dbContext.OrderDetails.ToListAsync();
            return _mapper.Map<IEnumerable<OrderDetailReadDTO>>(orderdetail);
        }

        public async Task<OrderDetailReadDTO> GetOrderDetailsByIdAsync(int id)
        {
            var orderdetail = await _dbContext.OrderDetails.FindAsync(id);
            return _mapper.Map<OrderDetailReadDTO>(orderdetail);
        }

        public async Task<bool> UpdateOrderDetailsAsync(OrderDetailUpdateDTO orderDetailUpdateDto,int id)
        {
            var existingdetail = await _dbContext.OrderDetails.FindAsync(id);
            if(existingdetail == null) return false;
            _mapper.Map(orderDetailUpdateDto, existingdetail);
            _dbContext.Update(existingdetail);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
