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
    public class CategoryService : ICategoryService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryService(ECommerceDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CategoryReadDTO> AddCategoriesAsync(CategoryCreateDTO categoryCreateDto)
        {
            var category = _mapper.Map<Categories>(categoryCreateDto);
             await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<CategoryReadDTO>(category);
           
        }

        public async Task<bool> DeleteCategoriesAsync(int id)
        {
           var category=await _dbContext.Categories.FindAsync(id);
            if(category == null) return false;            
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryReadDTO>> GetAllCategoriesAsync()
        {
            var category=await _dbContext.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryReadDTO>>(category);
        }

        public async Task<CategoryReadDTO> GetCategoriesByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            return _mapper.Map<CategoryReadDTO>(category);
        }

        public async Task<bool> UpdateCategoriesAsync(int id,CategoryUpdateDTO categoryUpdateDto)
        {
            var existingcategory = await _dbContext.Categories.FindAsync(id);
            if (existingcategory == null) return false;
            _mapper.Map(categoryUpdateDto, existingcategory);
            _dbContext.Update(existingcategory);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
