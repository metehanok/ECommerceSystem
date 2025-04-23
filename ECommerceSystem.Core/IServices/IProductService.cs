using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Service.DTO;



namespace ECommerceSystem.Core.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDTO>> GetAllProductsAsync();
        Task<ProductReadDTO> GetProductsByIdAsync(int id);
        Task<ProductReadDTO> AddProductsAsync(ProductCreateDTO productCreateDto);
        Task <bool> DeleteProductsAsync(int id);
        Task<bool> UpdateProductsAsync(int id,ProductUpdateDTO productUpdateDto);
    }
}
