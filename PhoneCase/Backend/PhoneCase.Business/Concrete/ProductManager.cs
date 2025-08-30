using System;
using System.Drawing;
using System.Linq.Expressions;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhoneCase.Business.Abstract;
using PhoneCase.Data.Abstract;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImageService _imageManager;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<Category> _categoryRepository;

    public ProductManager(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imageManager = imageManager;
        _productRepository = _unitOfWork.GetRepository<Product>();
        _categoryRepository = _unitOfWork.GetRepository<Category>();
    }

    public async Task<ResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
    {
        try
        {
            if (productCreateDto.CategoryIds.Count == 0)
            {
                return ResponseDto<ProductDto>.Fail("En az bir kategori seçilmelidir!", StatusCodes.Status400BadRequest);
            }
            foreach (var categoryId in productCreateDto.CategoryIds)
            {
                var isCategoryExits = await _categoryRepository.ExistsAsync(x => x.Id == categoryId);
                if (!isCategoryExits)
                {
                    return ResponseDto<ProductDto>.Fail("Bazı kategoriler veri tabanında  bulunamadığı için kayıt gerçekleştirilemedi!", StatusCodes.Status400BadRequest);
                }
            }
            var product = _mapper.Map<Product>(productCreateDto);

            if (productCreateDto.Image is null)
            {
                return ResponseDto<ProductDto>.Fail("Resim gönderilemediği için işlem tamamlanmadı!", StatusCodes.Status400BadRequest);
            }
            var imageUploadResult = await _imageManager.UploadAsync(productCreateDto.Image, "products");
            if (!imageUploadResult.IsSuccessful)
            {
                return ResponseDto<ProductDto>.Fail(imageUploadResult.Errors, imageUploadResult.StatusCode);
            }
            product.ImageUrl = imageUploadResult.Data;
            await _productRepository.AddAsync(product);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<ProductDto>.Fail("Beklenmedik bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            product.ProductCategories = productCreateDto
                               .CategoryIds
                               .Select(x => new ProductCategory(product.Id, x))
                               .ToList();
            _productRepository.Update(product);
            result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<ProductDto>.Fail("Beklenmedik bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            var productDto = _mapper.Map<ProductDto>(product);
            product = await _productRepository.GetAsync(
               predicate: x => x.Id == product.Id,
               includes: query => query.Include(x => x.ProductCategories).ThenInclude(y => y.Category)
            );
            productDto.Categories = product
                            .ProductCategories
                            .Select(pc => new CategoryDto
                            {
                                Id = pc.Category!.Id,
                                Name = pc.Category.Name,
                                ImageUrl = pc.Category.ImageUrl,
                                IsDeleted = pc.Category.IsDeleted,
                                Description = pc.Category.Description,
                                CreatedAt = TimeZoneInfo.ConvertTime(pc.Category.CreatedAt.UtcDateTime, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"))
                            }).ToList();
            return ResponseDto<ProductDto>.Success(productDto, StatusCodes.Status201Created);

        }
        catch (Exception ex)
        {
            return ResponseDto<ProductDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<int>> CountAsync(bool? isDeleted = null, int? categoryId = null)
    {
        try
        {
            var myIncludeDeleted = isDeleted != false;
            Expression<Func<Product, bool>> myPredicate = x => true;
            if (isDeleted.HasValue)
            {
                myPredicate = x => x.IsDeleted == isDeleted.Value;
            }
            if (categoryId.HasValue)
            {
                if (isDeleted.HasValue) myPredicate = x => x.IsDeleted == isDeleted && x.ProductCategories.Any(y => y.CategoryId == categoryId);
                else
                    myPredicate = x => x.ProductCategories.Any(y => y.CategoryId == categoryId); 
            }
            var count = await _productRepository.CountAsync(
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

    public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAllAsync(bool includeCategories = false, int? categoryId = null)
    {
        try
        {
            Expression<Func<Product, bool>> myPredicate = x => true;
            if (categoryId.HasValue)
            {
                myPredicate = x => x.ProductCategories.Any(x => x.CategoryId == categoryId);
            }
            var includeList = new List<Func<IQueryable<Product>, IQueryable<Product>>>();
            if (categoryId.HasValue && includeCategories)
            {
                includeList.Add(query => query.Include(x => x.ProductCategories));
            }
            if (includeCategories)
            {
                includeList.Add(query => query.Include(x => x.ProductCategories).ThenInclude(y => y.Category));
            }

            var products = await _productRepository.GetAllAsync(
                predicate: myPredicate,
                includes: includeList.ToArray()
            );

            if (!products.Any())
            {
                return ResponseDto<IEnumerable<ProductDto>>.Fail("Hiç ürün bulunamadı!", StatusCodes.Status404NotFound);
            }

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ResponseDto<IEnumerable<ProductDto>>.Success(productDtos, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<ProductDto>>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAllDeletedAsync()
    {
        try
        {
            var products = await _productRepository.GetAllAsync(
                predicate: x => x.IsDeleted,
                isDeleted: true
            );
            if (!products.Any())
            {
                return ResponseDto<IEnumerable<ProductDto>>.Fail("Hiç ana sayfa ürünü bulunamadı!", StatusCodes.Status404NotFound);
            }
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ResponseDto<IEnumerable<ProductDto>>.Success(productDtos, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<ProductDto>>.Fail($"Beklenmedik Hata: {ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAllHomePageAsync()
    {
        try
        {
            var products = await _productRepository.GetAllAsync(
                predicate: x => x.IsHome,
                includes: query => query.Include(x => x.ProductCategories).ThenInclude(y => y.Category)
            );
            if (!products.Any())
            {
                return ResponseDto<IEnumerable<ProductDto>>.Fail("Hiç ana sayfa ürünü bulunamadı!", StatusCodes.Status404NotFound);
            }
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ResponseDto<IEnumerable<ProductDto>>.Success(productDtos, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<ProductDto>>.Fail($"Beklenmedik Hata: {ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<ProductDto>> GetAsync(int id, bool includeCategories = false)
    {
        try
        {
            var includeList = new List<Func<IQueryable<Product>, IQueryable<Product>>>();
            if (includeCategories)
            {
                includeList.Add(query => query.Include(x => x.ProductCategories).ThenInclude(y => y.Category));
            }
            var product = await _productRepository.GetAsync(
                predicate: x => x.Id == id,
                includes: includeList.ToArray()
            );
            if (product is null)
            {
                return ResponseDto<ProductDto>.Fail("Ürün bulunamadı!", StatusCodes.Status404NotFound);
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return ResponseDto<ProductDto>.Success(productDto, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<ProductDto>.Fail($"Beklenmedik Hata: {ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContentDto>> HardDeletedAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetAsync(
                predicate: x => x.Id == id,
                includeDeleted: true
            );
            if (product is null)
            {
                return ResponseDto<NoContentDto>.Fail("Ürün bulunamadığı için silme işlemi gerçekleştirilemedi!", StatusCodes.Status404NotFound);
            }
            _productRepository.Delete(product);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContentDto>.Fail("Beklenmedik  bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            _imageManager.DeleteImage(product.ImageUrl!);
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata: {ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContentDto>> SoftDeletedAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetAsync(
                predicate: x => x.Id == id,
                includeDeleted: true
            );
            if (product is null)
            {
                return ResponseDto<NoContentDto>.Fail("Ürün bulunamadığı için işlemi gerçekleştirilemedi!", StatusCodes.Status404NotFound);
            }
            product.IsDeleted = !product.IsDeleted;
            if (product.IsDeleted)
            {
                product.IsHome = false;
            }
            product.DeletedAt = DateTimeOffset.UtcNow;
            _productRepository.Update(product);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContentDto>.Fail("Beklenmedik  bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata: {ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContentDto>> SoftDeletedByCategoryIdAsync(int categoryId)
    {
        try
        {
            var products = await _productRepository.GetAllAsync(
                predicate: x => x.ProductCategories.Any(y => y.CategoryId == categoryId)
            );
            if (!products.Any())
            {
                return ResponseDto<NoContentDto>.Fail("Bu kategoride hiç ürün bulunamadı!", StatusCodes.Status404NotFound);
            }
            foreach (var product in products)
            {
                product.IsDeleted = true;
                product.DeletedAt = DateTimeOffset.UtcNow;
            }
            _productRepository.BulkUpdate(products);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContentDto>.Fail("Beklenmedik bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata: {ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
    {
        try
        {
            var product = await _productRepository.GetAsync(
                predicate: x => x.Id == productUpdateDto.Id,
                includes: query => query.Include(x => x.ProductCategories)
            );
            if (product is null)
            {
                return ResponseDto<NoContentDto>.Fail("Ürün bulunamadı!", StatusCodes.Status404NotFound);
            }
            if (productUpdateDto.CategoryIds.Count == 0)
            {
                return ResponseDto<NoContentDto>.Fail("Kategori seçilmediği için güncelleme yapılamadı!", StatusCodes.Status400BadRequest);
            }
            foreach (var categoryId in productUpdateDto.CategoryIds)
            {
                var isCategoryExits = await _categoryRepository.ExistsAsync(x => x.Id == categoryId);
                if (!isCategoryExits) return ResponseDto<NoContentDto>.Fail("Bazı kategoriler veri tabanında bulunamadığı için kayıt gerçekleştirilmedi!", StatusCodes.Status400BadRequest);
            }
            var oldImageUrl = product.ImageUrl;
            if (productUpdateDto.Image is not null)
            {
                var imageUploadResult = await _imageManager.UploadAsync(productUpdateDto.Image, "products");
                if (!imageUploadResult.IsSuccessful)
                {
                    return ResponseDto<NoContentDto>.Fail(imageUploadResult.Errors, imageUploadResult.StatusCode);
                }
                product.ImageUrl = imageUploadResult.Data;
            }
            _mapper.Map(productUpdateDto, product);
            product.ProductCategories.Clear();
            product.ProductCategories = productUpdateDto
                          .CategoryIds
                          .Select(x => new ProductCategory(product.Id, x))
                          .ToList();
            product.UpdatedAt = DateTimeOffset.UtcNow;
            _productRepository.Update(product);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContentDto>.Fail("Beklenmedik bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            if (productUpdateDto.Image is not null) _imageManager.DeleteImage(oldImageUrl!);
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata: {ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContentDto>> UpdateIsHomeAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetAsync(
                predicate: x => x.Id == id
            );
            if (product is null)
            {
                return ResponseDto<NoContentDto>.Fail("Ürün bulunamadığı için işlemi gerçekleştirilemedi!", StatusCodes.Status404NotFound);
            }
            product.IsHome = !product.IsHome;
            product.DeletedAt = DateTimeOffset.UtcNow;
            _productRepository.Update(product);
            var result = await _unitOfWork.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContentDto>.Fail("Beklenmedik  bir hata oluştu!", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata: {ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }
}
