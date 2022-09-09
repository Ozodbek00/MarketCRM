using AutoMapper;
using Market.Domain.Entities;
using Market.Service.DTOs.OrderDtos;
using Market.Service.DTOs.ProductDtos;
using Market.Service.DTOs.PurchaseDtos;
using Market.Service.DTOs.UserDtos;

namespace Market.Service.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductForCreation>().ReverseMap();
            CreateMap<User, UserForCreation>().ReverseMap();
            CreateMap<Purchase, PurchaseForCreation>().ReverseMap();
            CreateMap<Order, OrderForCreation>().ReverseMap();
        }
    }
}
