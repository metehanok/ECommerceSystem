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
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerReadDTO>> GetAllCustomersAsync();
        Task<CustomerReadDTO> GetCustomersByIdAsync(int id);
        Task<bool> DeleteCustomerAsync(int id);
        Task<CustomerReadDTO>AddCustomersAsync(CustomerCreateDTO customerCreateDto);
        Task<bool> UpdateCustomersAsync(CustomerUpdateDTO customerUpdateDto,int id);
        Task<CustomerReadDTO> AuthenticateCustomerAsync(CustomerLoginDTO customerLoginDTO);
    }
}
