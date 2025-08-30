using System;
using AutoMapper;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Dtos.ProductDtos;

namespace PhoneCase.Business.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        var turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
        CreateMap<Product, ProductDto>()
         .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(src => TimeZoneInfo.ConvertTime(src.CreatedAt.UtcDateTime,
                turkeyTimeZone)))
                            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => TimeZoneInfo.ConvertTime(src.UpdatedAt.UtcDateTime,
                turkeyTimeZone)))
                            .ForMember(
                dest => dest.DeletedAt,
                opt => opt.MapFrom(src => TimeZoneInfo.ConvertTime(src.DeletedAt.UtcDateTime,
                turkeyTimeZone)))
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
                .ReverseMap();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();
    }
}
