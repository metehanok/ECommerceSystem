using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Service.DTO;

namespace ECommerceSystem.Core.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderReadDTO>> GetAllOrdersAsync();
        Task <OrderReadDTO> GetOrdersByIdAsync(int id);
        Task<OrderReadDTO> AddOrdersAsync (OrderCreateDTO orderCreateDto);
        Task<bool> UpdateOrdersAsync (OrderUpdateDTO orderUpdateDto,int id);
        Task<bool> DeleteOrdersAsync(int id);
    }
}
