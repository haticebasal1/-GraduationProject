using System;
using AutoMapper;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Dtos.CartDtos;

namespace PhoneCase.Business.Mappings;

public class CartProfile : Profile
{
    public CartProfile()
    {
        var turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
        CreateMap<Cart, CartDto>()
         .ForMember(
            dest => dest.User,
            opt => opt.MapFrom(src => src.User))
            .ForMember(
                dest => dest.CartItems,
                opt => opt.MapFrom(src => src.CartItems))
                .ReverseMap();
        CreateMap<CartItem, CartItemDto>()
        .ForMember(
            dest => dest.Product,
            opt => opt.MapFrom(src => src.Product))
            .ReverseMap();

        CreateMap<CartCreateDto, Cart>();
    }
}
