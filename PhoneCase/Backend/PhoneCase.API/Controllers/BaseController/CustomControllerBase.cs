using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.API.Controllers.BaseController;

public class CustomControllerBase : ControllerBase
{
   protected static IActionResult CreateResult<T>(ResponseDto<T> response)
    {
        return new ObjectResult(response) { StatusCode = response.StatusCode };
    }
    protected string UserId => User.FindFirst(ClaimTypes.NameIdentifier)!.Value;    
}
