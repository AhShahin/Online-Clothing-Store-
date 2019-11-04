using AutoMapper;
using OnlineStore.Dtos;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.Age, opt => {
                opt.MapFrom(src => src.DoB.CalculateAge());
            });
            CreateMap<User, UserForDetailedDto>()
            .ForMember(dest => dest.Age, opt => {
                opt.MapFrom(src => src.DoB.CalculateAge());
            });
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Address, AddressForListDto>();
            CreateMap<Address, AddressForDetailsDto>();
            CreateMap<AddressForUpdateDto, Address>();
            CreateMap<AddressForCreationDto, Address>();
            CreateMap<Category, CategoryForListDto>();
            CreateMap<Category, CategoryForDetailsDto>();
            CreateMap<CategoryForUpdateDto, Category>();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Item, ItemForListDto>();
            CreateMap<Item, ItemForDetailsDto>();
            CreateMap<Item, ItemsListForOrderDTO>();
            CreateMap<ItemForUpdateDto, Item>();
            CreateMap<ItemForCreationDto, Item>();
            CreateMap<Item_details, Item_detailsForListDto>();
            CreateMap<Item_details, Item_detailsForDetailsDto>();
            CreateMap<Item_detailsForUpdateDto, Item_details>();
            CreateMap<Item_detailsForCreationDto, Item_details>();
            CreateMap<Order, OrderForListDto>();
            CreateMap<Order, OrderForDetailsDto>().
                ForMember(dto => dto.Items, opt => opt.MapFrom(o => o.OrderItems.Select(oi => oi.Item).ToList()));
            CreateMap<OrderForUpdateDto, Order>();
            CreateMap<OrderForCreationDto, Order>();
            //CreateMap<OrderItem, OrderItemForDetailsDto>();
        }
    }
}
