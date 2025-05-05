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
            var orderDetail = await _dbContext.OrderDetails
        .Include(od => od.Products)
        .Include(od => od.Orders)
        .FirstOrDefaultAsync(od => od.Id == id);

            if (orderDetail == null) return null;

            return new OrderDetailReadDTO
            {
                Id = orderDetail.Id,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                TotalPrice = (int)orderDetail.TotalPrice,
                ProductId = orderDetail.ProductId,
                ProductName = orderDetail.Products.Name,
                OrderId = orderDetail.OrderId
            };
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
        public async Task<IEnumerable<OrderDetailReadDTO>> GetOrderDetailsByCustomerIdAsync(int customerId)
        {
            var orderDetails = await _dbContext.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Products)
                .SelectMany(o => o.OrderDetails.Select(od => new OrderDetailReadDTO
                {
                    Id = od.Id,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    TotalPrice = (int)od.TotalPrice,
                    ProductId = od.ProductId,
                    ProductName = od.Products.Name,
                    OrderId = o.Id,
                    Orders = o
                }))
                .ToListAsync();

            return orderDetails;
        }
        public async Task<IEnumerable<OrderDetailReadDTO>> GetOrderDetailsByOrderId(int orderId)
        {
            var orderdetails = await _dbContext.OrderDetails.Include(od => od.Products)
                .Where(od => od.OrderId == orderId).ToListAsync();
            if (orderdetails == null || !orderdetails.Any())
                return null;

            var result = orderdetails.Select(od => new OrderDetailReadDTO
            {
                Id = od.Id,
                Quantity = od.Quantity,
                Price = od.Price,
                ProductId = od.ProductId,
                ProductName = od.Products?.Name,
                OrderId = od.OrderId,
                TotalPrice = (int)od.TotalPrice
            }).ToList();

            return result;
        }

       
    }
}
