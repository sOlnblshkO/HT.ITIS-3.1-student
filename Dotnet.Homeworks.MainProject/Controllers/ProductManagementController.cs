using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

public class ProductManagementController : ControllerBase
{
    
    [HttpGet("getProducts")]
    public async Task<IActionResult> GetProducts()
    {
        throw new NotImplementedException();
    }

    [HttpPost("insertProduct")]
    public async Task<IActionResult> InsertProduct(string name)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("deleteProduct")]
    public async Task<IActionResult> DeleteProduct(Guid guid)
    {
        throw new NotImplementedException();
    }

    [HttpPut("updateProduct")]
    public async Task<IActionResult> UpdateProduct(Guid guid, string name)
    {
        throw new NotImplementedException();
    }
}