using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

public class ProductManagementController : ControllerBase
{
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        throw new NotImplementedException();
    }

    [HttpPost("product")]
    public async Task<IActionResult> InsertProduct(string name)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("product")]
    public async Task<IActionResult> DeleteProduct(Guid guid)
    {
        throw new NotImplementedException();
    }

    [HttpPut("product")]
    public async Task<IActionResult> UpdateProduct(Guid guid, string name)
    {
        throw new NotImplementedException();
    }
}