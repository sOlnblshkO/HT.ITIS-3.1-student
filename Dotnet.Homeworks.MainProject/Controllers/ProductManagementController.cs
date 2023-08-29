using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

[ApiController]
public class ProductManagementController : ControllerBase
{
    [HttpGet("products")]
    public Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost("product")]
    public Task<IActionResult> InsertProduct(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("product")]
    public Task<IActionResult> DeleteProduct(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut("product")]
    public Task<IActionResult> UpdateProduct(Guid guid, string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}