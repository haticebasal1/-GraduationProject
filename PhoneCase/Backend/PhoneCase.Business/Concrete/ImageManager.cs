using System;
using Microsoft.AspNetCore.Http;
using PhoneCase.Business.Abstract;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Concrete;

public class ImageManager : IImageService
{
    private readonly string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

    public ResponseDto<NoContentDto> DeleteImage(string imageUrl)
    {
        try
        {
            var deletedImageFullPath = Path.Combine(_imageFolderPath, imageUrl);
            File.Delete(deletedImageFullPath);
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);

        }
        catch (Exception ex)
        {
            return ResponseDto<NoContentDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public ResponseDto<bool> ImageExists(string imageUrl)
    {
        try
        {
            var imageFullPath = Path.Combine(_imageFolderPath, imageUrl);
            var result = File.Exists(imageFullPath);
            return ResponseDto<bool>.Success(result, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<bool>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<string>> UploadAsync(IFormFile image, string folderName)
    {
        try
        {
            if (image is null)
            {
                return ResponseDto<string>.Fail("Resim gönderilmediği için yüklenemedi!", StatusCodes.Status400BadRequest);
            }
            if (image.Length == 0 || image.Length > 5 * 1024 * 1024)
            {
                return ResponseDto<string>.Fail("Resim 5 mb'tan küçük olmadığı için yüklenemedi!", StatusCodes.Status400BadRequest);
            }
            string[] allowedExtensions = [".png", ".jpg", ".jpeg"];
            var imageExtension = Path.GetExtension(image.FileName);
            if (!allowedExtensions.Contains(imageExtension))
            {
                return ResponseDto<string>.Fail("Geçersiz resim formatı", StatusCodes.Status400BadRequest);
            }
            var folderPath = Path.Combine(_imageFolderPath, folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = $"{Guid.NewGuid()}{imageExtension}";
            var fileFullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fileFullPath, FileMode.Create);
            await image.CopyToAsync(stream);
            var imageUrl = Path.Combine(folderName, fileName);
            return ResponseDto<string>.Success(imageUrl, StatusCodes.Status201Created);

        }
        catch (Exception ex)
        {
            return ResponseDto<string>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }
}
