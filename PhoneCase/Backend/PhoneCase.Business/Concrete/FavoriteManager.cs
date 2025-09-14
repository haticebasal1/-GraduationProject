using System;
using System.Linq.Expressions;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhoneCase.Business.Abstract;
using PhoneCase.Data;
using PhoneCase.Data.Abstract;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Dtos.FavoriteDtos;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Concrete;

public class FavoriteManager : IFavoriteService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Favorite> _favoriteRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public FavoriteManager(IUnitOfWork unitOfWork, IGenericRepository<Favorite> favoriteRepository, IGenericRepository<User> userRepository, IGenericRepository<Product> productRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _favoriteRepository = _unitOfWork.GetRepository<Favorite>();
        _userRepository = _unitOfWork.GetRepository<User>();
        _productRepository = _unitOfWork.GetRepository<Product>();
        _mapper = mapper;
    }

public async Task<ResponseDto<FavoriteDto>> AddAsync(FavoriteCreateDto favoriteCreateDto)
{
    try
    {
        var isUserExists = await _userRepository.ExistsAsync(x => x.Id == favoriteCreateDto.UserId);
        if (!isUserExists)
            return ResponseDto<FavoriteDto>.Fail("Kullanıcı bulunamadı!", StatusCodes.Status404NotFound);
        var product = await _productRepository.GetAsync(x => x.Id == favoriteCreateDto.ProductId);
        if (product == null)
            return ResponseDto<FavoriteDto>.Fail("Ürün bulunamadı!", StatusCodes.Status404NotFound);
        var favorite = new Favorite
        {
            UserId = favoriteCreateDto.UserId!,
            ProductId = product.Id,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            ProductName = product.Name!,
            Price = product.Price,
            ImageUrl = product.ImageUrl!
        };
        await _favoriteRepository.AddAsync(favorite);
        await _unitOfWork.SaveAsync();
        var result = _mapper.Map<FavoriteDto>(favorite);
        return ResponseDto<FavoriteDto>.Success(result, StatusCodes.Status200OK);
    }
        catch (Exception ex)
        {
            return ResponseDto<FavoriteDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
}
    public async Task<ResponseDto<NoContentDto>> DeleteAsync(int id)
    {
        try
        {
            var favorite = await _favoriteRepository.GetAsync(x => x.Id == id);
            if (favorite is null)
            {
                return ResponseDto<NoContentDto>.Fail("Favori bulunamadı!", StatusCodes.Status404NotFound);
            }
            _favoriteRepository.Delete(favorite);
            await _unitOfWork.SaveAsync();
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }


public async Task<ResponseDto<IEnumerable<FavoriteDto>>> GetAllAsync(
    string userId = null!,
    int productId = 0,
    bool includeUser = false,
    bool includeProduct = false,
    bool? isDeleted = null)
{
    try
    {
        var includeList = new List<Func<IQueryable<Favorite>, IQueryable<Favorite>>>();
        if (includeUser)
            includeList.Add(q => q.Include(f => f.User));
        if (includeProduct)
            includeList.Add(q => q.Include(f => f.Product));
        var favorites = await _favoriteRepository.GetAllAsync(
            predicate: x =>
                (string.IsNullOrEmpty(userId) || x.UserId == userId) &&
                (productId == 0 || x.ProductId == productId) &&
                (!isDeleted.HasValue || x.IsDeleted == isDeleted.Value),
            includes: includeList.ToArray()
        );

        if (!favorites.Any())
            return ResponseDto<IEnumerable<FavoriteDto>>.Fail(
                "Favori bulunamadı!", StatusCodes.Status404NotFound);
        var favoriteDtos = favorites.Select(f => new FavoriteDto
        {
            Id = f.Id,
            UserId = f.UserId,
            ProductId = f.ProductId,
            CreatedDate = f.CreatedDate,
            UpdatedDate = f.UpdatedDate,
            ProductName = f.Product != null ? f.Product.Name! : f.ProductName,
            Price = f.Product != null ? f.Product.Price : f.Price,
            ImageUrl = f.Product != null ? f.Product.ImageUrl! : f.ImageUrl
        }).ToList();

        return ResponseDto<IEnumerable<FavoriteDto>>.Success(favoriteDtos, StatusCodes.Status200OK);
    }
        catch (Exception ex)
        {
            return ResponseDto<IEnumerable<FavoriteDto>>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
}


    public async Task<ResponseDto<FavoriteDto>> GetByIdAsync(int id)
    {
        try
        {
            var favorite = await _favoriteRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (favorite is null)
            {
                return ResponseDto<FavoriteDto>.Fail("Favori bulunamadı!", StatusCodes.Status404NotFound);
            }
            var result = _mapper.Map<FavoriteDto>(favorite);
            return ResponseDto<FavoriteDto>.Success(result, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<FavoriteDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContentDto>> SoftDeletedAsync(int id)
    {
        try
        {
            var favorite = await _favoriteRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (favorite is null)
            {
                return ResponseDto<NoContentDto>.Fail("Favori bulunamadı veya zaten silinmiş", StatusCodes.Status404NotFound);
            }

            favorite.IsDeleted = true;
            favorite.DeletedAt = DateTime.UtcNow;

            _favoriteRepository.Update(favorite);
            await _unitOfWork.SaveAsync();
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }

        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<FavoriteDto>> UpdateAsync(FavoriteUpdateDto favoriteUpdateDto)
    {
        try
        {
            var favorite = await _favoriteRepository.GetAsync(
             x => x.Id == favoriteUpdateDto.Id
            );
            if (favorite == null)
            {
                return ResponseDto<FavoriteDto>.Fail("Favori bulunamadı!", StatusCodes.Status404NotFound);
            }
            if (favorite.UserId is null)
            {
                return ResponseDto<FavoriteDto>.Fail("Favoriye ait kullanıcı bulunamadı!", StatusCodes.Status404NotFound);
            }
            if (favorite.ProductId == 0)
            {
                return ResponseDto<FavoriteDto>.Fail("Favoriye ait ürün bulunamadı!", StatusCodes.Status404NotFound);
            }
            favorite.UserId = favoriteUpdateDto.UserId;
            favorite.ProductId = favoriteUpdateDto.ProductId;
            favorite.UpdatedAt = DateTime.UtcNow;

            _favoriteRepository.Update(favorite);
            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<FavoriteDto>(favorite);
            return ResponseDto<FavoriteDto>.Success(result, StatusCodes.Status200OK);

        }
        catch (Exception ex)
        {
            return ResponseDto<FavoriteDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }
}
