using AssetManagement.Application.Models.DTOs.Assets;
using AssetManagement.Application.Models.DTOs.Assets.Requests;
using AssetManagement.Application.Models.DTOs.Assets.Responses;
using AssetManagement.Application.Models.DTOs.Assignments;
using AssetManagement.Application.Models.DTOs.Assignments.Request;
using AssetManagement.Application.Models.DTOs.Category;
using AssetManagement.Application.Models.DTOs.Category.Requests;
using AssetManagement.Application.Models.DTOs.ReturnRequests;
using AssetManagement.Application.Models.DTOs.ReturnRequests.Request;
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
                .ReverseMap(); ;
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

            //ReturnRequest Mapping 
            CreateMap<ReturnRequest, ReturnRequestDto>().ReverseMap();
            CreateMap<AddReturnRequestDto, ReturnRequest>().ReverseMap();
        }
    }
}