using System;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.API.Controllers.BaseController;

public class CustomControllerBase : ControllerBase
{
    public static IActionResult CreateResult<T>(ResponseDto<T> response)
    {
        return new ObjectResult(response) { StatusCode = response.StatusCode };
    }
}
