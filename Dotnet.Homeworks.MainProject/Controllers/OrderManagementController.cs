using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

[ApiController]
public class OrderManagementController : ControllerBase
{
    [HttpGet("orders")]
    public async Task<IActionResult> GetUserOrdersAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("order/{id:guid}")]
    public async Task<IActionResult> GetUserOrdersAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost("order")]
    public async Task<IActionResult> CreateOrderAsync([FromBody] IEnumerable<Guid> productsIds)
    {
        throw new NotImplementedException();
    }

    [HttpPut("order")]
    public async Task<IActionResult> UpdateOrderAsync([FromBody] Guid orderId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("order/{id:guid}")]
    public async Task<IActionResult> DeleteOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}