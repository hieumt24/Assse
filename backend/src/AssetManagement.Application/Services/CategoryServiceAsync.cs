using AssetManagement.Application.Interfaces;
using AssetManagement.Application.Models.DTOs.Category;
using AssetManagement.Application.Models.DTOs.Category.Requests;
using AssetManagement.Application.Wrappers;
using AssetManagement.Domain.Entites;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Application.Services
{
    public class CategoryServiceAsync : ICategoryServiceAsync
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepositoriesAsync _categoryRepository;
        //private readonly IValidator<AddCategoryRequestDto> _addCategoryValidator;
        //private readonly IValidator<CategoryDto> _editCategoryValidator;

        public CategoryServiceAsync(
            ICategoryRepositoriesAsync categoryRepository,
            IMapper mapper
            //IValidator<AddCategoryRequestDto> addCategoryValidator,
            //IValidator<CategoryDto> editCategoryValidator
        )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            //_addCategoryValidator = addCategoryValidator;
            //_editCategoryValidator = editCategoryValidator;
        }

        public async Task<Response<CategoryDto>> AddCategoryAsync(AddCategoryRequestDto request)
        {
            //var validationResult = await _addCategoryValidator.ValidateAsync(request);
            //if (!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //    return new Response<CategoryDto> { Succeeded = false, Errors = errors };
            //}

            try
            {
                if (await _categoryRepository.IsCategoryNameDuplicateAsync(request.CategoryName))
                {
                    return new Response<CategoryDto> { Succeeded = false, Message = "Category name already exists." };
                }

                if (await _categoryRepository.IsPrefixDuplicateAsync(request.Prefix))
                {
                    return new Response<CategoryDto> { Succeeded = false, Message = "Category prefix already exists." };
                }

                var category = _mapper.Map<Category>(request);
                var addedCategory = await _categoryRepository.AddAsync(category);

                var categoryDto = _mapper.Map<CategoryDto>(addedCategory);
                return new Response<CategoryDto> { Succeeded = true, Data = categoryDto };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<CategoryDto>> GetCategoryByIdAsync(Guid categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return new Response<CategoryDto> { Succeeded = false, Message = "Category not found." };
                }

                var categoryDto = _mapper.Map<CategoryDto>(category);
                return new Response<CategoryDto> { Succeeded = true, Data = categoryDto };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<CategoryDto>> EditCategoryAsync(UpdateCategoryRequestDto request)
        {
            //var validationResult = await _editCategoryValidator.ValidateAsync(request);
            //if (!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //    return new Response<CategoryDto> { Succeeded = false, Errors = errors };
            //}

            try
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(request.Id);
                if (existingCategory == null)
                {
                    return new Response<CategoryDto> { Succeeded = false, Message = "Category not found." };
                }

                if (await _categoryRepository.IsCategoryNameDuplicateAsync(request.CategoryName))
                {
                    return new Response<CategoryDto> { Succeeded = false, Message = "Category name already exists." };
                }

                if (await _categoryRepository.IsPrefixDuplicateAsync(request.Prefix))
                {
                    return new Response<CategoryDto> { Succeeded = false, Message = "Category prefix already exists." };
                }

                existingCategory.CategoryName = request.CategoryName;
                existingCategory.Prefix = request.Prefix;
                await _categoryRepository.UpdateAsync(existingCategory);

                var updatedCategoryDto = _mapper.Map<CategoryDto>(existingCategory);
                return new Response<CategoryDto> { Succeeded = true, Data = updatedCategoryDto };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<CategoryDto>> DeleteCategoryAsync(Guid categoryId)
        {
            try
            {
                var category = await _categoryRepository.DeleteAsync(categoryId);
                if (category == null)
                {
                    return new Response<CategoryDto> { Succeeded = false, Message = "Category not found." };
                }

                var categoryDto = _mapper.Map<CategoryDto>(category);
                return new Response<CategoryDto> { Succeeded = true, Data = categoryDto };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDto> { Succeeded = false, Errors = { ex.Message } };
            }
        }

        public async Task<Response<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.ListAllActiveAsync();
                var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
                return new Response<List<CategoryDto>> { Succeeded = true, Data = categoryDtos };
            }
            catch (Exception ex)
            {
                return new Response<List<CategoryDto>> { Succeeded = false, Errors = { ex.Message } };
            }
        }
    }
}
