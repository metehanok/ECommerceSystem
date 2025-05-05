using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Core.DTO;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Service.DTO;

namespace ECommerceSystem.Core.IServices
{
    public interface IOrderDetailsService
    {
        Task<IEnumerable<OrderDetailReadDTO>> GetAllOrdersAsync();
        Task<OrderDetailReadDTO> GetOrderDetailsByIdAsync(int id);
        Task<OrderDetailReadDTO> AddOrderDetailsAsync(OrderDetailCreateDTO orderDetailCreateDto);
        Task<bool> UpdateOrderDetailsAsync(OrderDetailUpdateDTO orderDetailUpdateDto,int id);
        Task<bool> DeleteOrderDetailsAsync(int id);
        Task<IEnumerable<OrderDetailReadDTO>> GetOrderDetailsByCustomerIdAsync(int customerid);
        Task<IEnumerable<OrderDetailReadDTO>> GetOrderDetailsByOrderId(int orderId);
    }
}
