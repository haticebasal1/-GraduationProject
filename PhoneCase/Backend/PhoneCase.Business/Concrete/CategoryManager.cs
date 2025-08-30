using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using PhoneCase.Business.Abstract;
using PhoneCase.Data.Abstract;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;
    private readonly IImageService _imageManager;
    public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageManager)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = _unitOfWork.GetRepository<Category>();
        _productCategoryRepository = _unitOfWork.GetRepository<ProductCategory>();
        _mapper = mapper;
        _imageManager = imageManager;
    }

    public async Task<ResponseDto<CategoryDto>> AddAsync(CategoryCreateDto categoryCreateDto)
    {
        try
        {
            var isExists = await _categoryRepository.ExistsAsync(x => x.Name!.ToLower() == categoryCreateDto.Name.ToLower());
            if (isExists)
            {
                return ResponseDto<CategoryDto>.Fail("Bu isimde bir kategori mevcut olduğu için işlem başarısız oldu!", StatusCodes.Status400BadRequest);
            }
            var category = _mapper.Map<Category>(categoryCreateDto);

            if (categoryCreateDto.Type == null)
            {
                return ResponseDto<CategoryDto>.Fail("Kategori tipi girilmediği için işlem başarısız oldu!", StatusCodes.Status400BadRequest);
            }

            if (categoryCreateDto.Image is null)
            {
                return ResponseDto<CategoryDto>.Fail("Resim gönderilmediği için işlem tamamlanamadı!", StatusCodes.Status400BadRequest);
            }
            var imageUploadResult = await _imageManager.UploadAsync(categoryCreateDto.Image, "categories");
            if (!imageUploadResult.IsSuccessful)
            {
                return ResponseDto<CategoryDto>.Fail(imageUploadResult.Errors, imageUploadResult.StatusCode);
            }
            category.ImageUrl = imageUploadResult.Data;

            await _categoryRepository.AddAsync(category);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<CategoryDto>.Fail("Beklenmedik bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {

            return ResponseDto<CategoryDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<int>> CountAsync(bool? isDeleted = false)
    {
        try
        {
            var myIncludeDeleted = isDeleted != false;
            Expression<Func<Category, bool>> myPredicate = x => true;
            if (isDeleted.HasValue)
            {
                myPredicate = x => x.IsDeleted == isDeleted.Value;
            }
            var count = await _categoryRepository.CountAsync(
                predicate: myPredicate,
                includeDeleted: myIncludeDeleted
            );
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<int>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<IEnumerable<CategoryDto>>> GetAllAsync(bool? isDeleted = false)
    {
        try
        {
            var myIncludeDeleted = isDeleted != false;
            Expression<Func<Category, bool>> myPredicate = x => true;
            if (isDeleted.HasValue)
            {
                myPredicate = x => x.IsDeleted == isDeleted.Value;
            }
            var categories = await _categoryRepository.GetAllAsync(
                predicate: myPredicate,
                isDeleted: isDeleted
            );
            if (!categories.Any())
            {
                return ResponseDto<IEnumerable<CategoryDto>>.Fail("Hiç kategori bilgisi bulunamadı!", StatusCodes.Status404NotFound);
            }
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return ResponseDto<IEnumerable<CategoryDto>>.Success(categoryDtos, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<CategoryDto>>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }
    public async Task<ResponseDto<CategoryDto>> GetAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);
            if (category is null)
            {
                return ResponseDto<CategoryDto>.Fail($"{id} id'li kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<CategoryDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }
    public async Task<ResponseDto<NoContentDto>> HardDeleteAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(
                predicate: x => x.Id == id,
                includeDeleted: true
            );
            if (category is null)
            {
                return ResponseDto<NoContentDto>.Fail($"{id} id'li kategori bulunamadığı için silme işlemi gerçekleştirilemedi!", StatusCodes.Status404NotFound);
            }
            var hasProducts = await _productCategoryRepository.ExistsAsync(x => x.CategoryId == id);
            if (hasProducts)
            {
                return ResponseDto<NoContentDto>.Fail("Bu kategoride ürünler mevcut olduğu için silme işlemi gerçekleştirilemedi!", StatusCodes.Status400BadRequest);
            }
            _categoryRepository.Delete(category);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContentDto>.Fail($"Beklenmedik bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            if (category.ImageUrl is not null)
            {
                _imageManager.DeleteImage(category.ImageUrl);

            }
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);

        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContentDto>> SoftDeleteAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(
                predicate: x => x.Id == id,
                includeDeleted: true
            );
            if (category is null)
            {
                return ResponseDto<NoContentDto>.Fail($"{id} id'li kategori bulunamadığı için işlem gerçekleştirilemedi!", StatusCodes.Status404NotFound);
            }
            var hasProducts = await _productCategoryRepository.ExistsAsync(x => x.CategoryId == id);
            if (hasProducts)
            {
                return ResponseDto<NoContentDto>.Fail("Bu kategoride ürünler mevcut olduğu için silme işlemi gerçekleştirilemedi!", StatusCodes.Status400BadRequest);
            }
            category.IsDeleted = !category.IsDeleted;
            category.DeletedAt = DateTimeOffset.UtcNow;
            _categoryRepository.Update(category);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContentDto>.Fail($"Beklenmedik bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);

        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContentDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == categoryUpdateDto.Id);
            if (category is null)
            {
                return ResponseDto<NoContentDto>.Fail($"{categoryUpdateDto.Id} id'li kategori bulunamadığı için güncelleme işlemi gerçekleştirilemedi!", StatusCodes.Status404NotFound);
            }
            var oldImageUrl = category.ImageUrl;
            if (categoryUpdateDto.Image is not null)
            {
                var imageUploadResult = await _imageManager.UploadAsync(categoryUpdateDto.Image, "categories");
                if (!imageUploadResult.IsSuccessful)
                {
                    return ResponseDto<NoContentDto>.Fail(imageUploadResult.Errors, imageUploadResult.StatusCode);
                }
                category.ImageUrl = imageUploadResult.Data;
            }
            _mapper.Map(categoryUpdateDto, category);
            category.UpdatedAt = DateTimeOffset.UtcNow;
            _categoryRepository.Update(category);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContentDto>.Fail($"Beklenmedik bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            if (categoryUpdateDto.Image is not null)
            {
                _imageManager.DeleteImage(oldImageUrl!);
            }
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }
}
