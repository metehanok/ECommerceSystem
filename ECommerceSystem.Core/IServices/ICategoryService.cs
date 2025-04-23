using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Service.DTO;

namespace ECommerceSystem.Core.IServices
{
    public  interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDTO>> GetAllCategoriesAsync();
        Task<CategoryReadDTO> GetCategoriesByIdAsync(int id);

        Task<CategoryReadDTO> AddCategoriesAsync(CategoryCreateDTO categoryCreateDto);
        Task<bool> UpdateCategoriesAsync(int id,CategoryUpdateDTO categoryUpdateDto);

        Task<bool> DeleteCategoriesAsync(int id);

    }
}
