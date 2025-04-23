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
    public class ProductService : IProductService
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductService(ECommerceDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public async Task<ProductReadDTO> AddProductsAsync(ProductCreateDTO productCreateDto)
        {
            var product = _mapper.Map<Products>(productCreateDto);
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductReadDTO>(product);
           
        }

        public async Task<bool> DeleteProductsAsync(int id)
        {
            var products= await _dbContext.Products.FindAsync(id);
            if (products == null)return false;            
            _dbContext.Products.Remove(products);
            await _dbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<IEnumerable<ProductReadDTO>> GetAllProductsAsync()
        {
            var product= await _dbContext.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductReadDTO>>(product);
        }

        public async Task<ProductReadDTO> GetProductsByIdAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            return _mapper.Map<ProductReadDTO>(product);

        }

        public async Task<bool> UpdateProductsAsync(int id,ProductUpdateDTO productUpdateDto)
        {
            var existinproduct = await _dbContext.Products.FindAsync(id);
            if (existinproduct == null) return false;
            _mapper.Map(productUpdateDto, existinproduct);
             
             _dbContext.Update(existinproduct);
            await _dbContext.SaveChangesAsync();
            return true;
        }

       
    }
}
