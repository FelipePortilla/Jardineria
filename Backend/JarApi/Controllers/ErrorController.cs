
using ApiApolo.Controllers;
using JarApi.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;


namespace JarApi.Controllers;

[Microsoft.AspNetCore.Components.Route("errors/{code}")]
public class ErroController:BaseController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}