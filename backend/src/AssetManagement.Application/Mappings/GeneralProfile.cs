using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Assets.Responses;
using AssetManagement.Application.Models.DTOs.Assignment;
using AssetManagement.Application.Models.DTOs.Assignment.Request;
using AssetManagement.Application.Models.DTOs.Assignment.Response;
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
            CreateMap<EditAssetRequestDto, Asset>().ReverseMap();
            CreateMap<Asset, AssetDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();
            CreateMap<Asset, AssetResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();

            //Category Mapping
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<CategoryDto, UpdateCategoryRequestDto>().ReverseMap();

            //Assignment Mapping
            CreateMap<Assignment, AssignmentDto>().ReverseMap();
            CreateMap<AddAssignmentRequestDto, Assignment>().ReverseMap();
            CreateMap<Assignment, AssignmentResponseDto>()
                .ForMember(dest => dest.AssetCode, opt => opt.MapFrom(src => src.Asset.AssetCode))
                .ForMember(dest => dest.AssetName, opt => opt.MapFrom(opt => opt.Asset.AssetName))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(opt => opt.AssignedTo.Username))
                .ForMember(dest => dest.AssignedBy, opt => opt.MapFrom(opt => opt.AssignedBy.Username))
                .ReverseMap()
                ;
        }
    }
}