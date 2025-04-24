using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Service.DTO.HelperDTO;

namespace ECommerceSystem.Service
{
    public class DTOMapper:Profile
    {
        public DTOMapper()
        {
            CreateMap<Customers,CustomerReadDTO>();
            CreateMap<CustomerUpdateDTO, Customers>();
            CreateMap<CustomerCreateDTO,Customers>();
            CreateMap<CustomerCreateDTO, Customers>();

            CreateMap<Products,ProductReadDTO>().ForMember(x=>x.CategoryName,opt=>opt.MapFrom(z=>z.Category.Name));
            CreateMap<ProductCreateDTO,Products>();
            CreateMap<ProductUpdateDTO,Products>();
            CreateMap<ProductCreateDTO,Products>();

            CreateMap<Categories, CategoryReadDTO>();
            CreateMap<CategoryReadDTO,Categories>();
            CreateMap<CategoryUpdateDTO,Categories>();
            CreateMap<CategoryCreateDTO,Categories>();

            CreateMap<Orders, OrderReadDTO>();
            CreateMap<OrderDetails, OrderDetailDTO>()  // OrderDetails ve OrderDetailDTO arasında eşleme ekledik
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Price)); // TotalPrice hesaplaması
            CreateMap<OrderCreateDTO,Orders>();
            CreateMap<OrderUpdateDTO,Orders>();
            CreateMap<OrderCreateDTO,Orders>();

            CreateMap<OrderDetails, OrderDetailReadDTO>().ForMember(x=>x.ProductName,opt=>opt.MapFrom(x=>x.Products.Name));
            CreateMap<OrderDetailCreateDTO,OrderDetails>();
            CreateMap<OrderDetailCreateDTO,OrderDetails>();


        }
    }
}
