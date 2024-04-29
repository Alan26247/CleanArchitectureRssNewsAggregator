using Microsoft.AspNetCore.Mvc;
using Web.Common.Dtos.CommonDtos;

namespace Web.Common.Extensions;

public static class ControllerExtensions
{
    public static IActionResult GetResponse<T>(this ControllerBase controller, int statusCode, T data)
    {
        return controller.StatusCode(statusCode, new ResponseDataDto<T>
        {
            StatusCode = statusCode,
            Data = data
        });
    }
}
