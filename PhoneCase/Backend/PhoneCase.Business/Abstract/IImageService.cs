using System;
using Microsoft.AspNetCore.Http;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Abstract;

public interface IImageService
{
    Task<ResponseDto<string>> UploadAsync(IFormFile image, string folderName);
    ResponseDto<NoContentDto> DeleteImage(string imageUrl);
    ResponseDto<bool> ImageExists(string imageUrl);
}
