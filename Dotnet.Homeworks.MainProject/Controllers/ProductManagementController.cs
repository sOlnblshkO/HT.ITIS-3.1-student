using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

public class ProductManagementController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProductManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
    public async Task<IActionResult> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPut("updateProduct")]
    public async Task<IActionResult> UpdateProduct(Guid id, string name)
    {
        throw new NotImplementedException();
    }
}