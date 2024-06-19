using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Assets.Responses;
using AssetManagement.Application.Models.DTOs.Category;
using AssetManagement.Application.Models.DTOs.Category.Requests;
using AssetManagement.Application.Models.DTOs.Users;
using AssetManagement.Application.Models.DTOs.Users.Requests;
using AssetManagement.Application.Models.DTOs.Users.Responses;
using AssetManagement.Domain.Entites;
using AutoMapper;

namespace AssetManagement.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<UserDto, UpdateUserRequestDto>().ReverseMap();
            CreateMap<AddUserRequestDto, User>().ReverseMap();

            //Asset Mapping
            CreateMap<AddAssetRequestDto, Asset>().ReverseMap();
            CreateMap<Asset, AssetDto>().ReverseMap();
            CreateMap<Asset, AssetResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();

            //Category Mapping
            CreateMap<Category, CategoryDto>();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<CategoryDto, UpdateCategoryRequestDto>().ReverseMap();
        }
    }
}