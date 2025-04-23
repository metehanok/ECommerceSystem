using AutoMapper;
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
    public class OrderService : IOrderService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(ECommerceDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<OrderReadDTO> AddOrdersAsync(OrderCreateDTO orderCreateDto)
        {
            if (orderCreateDto.Products == null || orderCreateDto.Products.Count == 0)
            {
                throw new ArgumentException("Product tablosu boş olabilir.");
            }
           
            var order = _mapper.Map<Orders>(orderCreateDto);
           
            var orderDetails = new List<OrderDetails>();
          
            foreach (var productDto in orderCreateDto.Products)
            {
                // Ürün ID'sine göre ürünü veritabanından buluyoruz
                var product = await _dbContext.Products.FindAsync(productDto.ProductId);

                if (product == null)
                {
                    throw new Exception($"Şu ID' deki :{productDto.ProductId} ürün bulunamadı.");
                }
               
                var orderDetail = new OrderDetails
                {
                    ProductId = product.Id,
                    Quantity = productDto.Quantity,
                    Price = product.Price,
                    TotalPrice = product.Price * productDto.Quantity
                };                              
                decimal totalPrice = product.Price * productDto.Quantity;                            
                orderDetails.Add(orderDetail);
            }            
            order.OrderDetails = orderDetails;
            
            await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();           
            return _mapper.Map<OrderReadDTO>(order);
        }


        public async Task<bool> DeleteOrdersAsync(int id)
        {
           var orders= await _dbContext.Orders.FindAsync(id);
            if (orders == null) return false;            
            _dbContext.Orders.Remove(orders);
            await _dbContext.SaveChangesAsync();
            
            return true;
            
        }

        public async Task<IEnumerable<OrderReadDTO>> GetAllOrdersAsync()
        {
            var orders=await _dbContext.Orders.ToListAsync();
            return _mapper.Map<IEnumerable<OrderReadDTO>>(orders);  


        }

        public async Task<OrderReadDTO> GetOrdersByIdAsync(int id)
        {
            var orders = await _dbContext.Orders.FindAsync(id);
            return _mapper.Map<OrderReadDTO>(orders);
            
        }

        public async Task<bool> UpdateOrdersAsync(OrderUpdateDTO orderUpdateDto,int id)
        {
            var existingorders = await _dbContext.Orders.FindAsync(id);
            if(existingorders == null) return false;
            _mapper.Map(orderUpdateDto, existingorders);
            _dbContext.Orders.Update(existingorders);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
