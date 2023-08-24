using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

[ApiController]
public class OrderManagementController : ControllerBase
{
    [HttpGet("orders")]
    public Task<IActionResult> GetUserOrdersAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("order/{id:guid}")]
    public Task<IActionResult> GetUserOrdersAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPost("order")]
    public Task<IActionResult> CreateOrderAsync([FromBody] IEnumerable<Guid> productsIds,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut("order/{id:guid}")]
    public Task<IActionResult> UpdateOrderAsync(Guid id, [FromBody] IEnumerable<Guid> productsIds,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("order/{id:guid}")]
    public Task<IActionResult> DeleteOrderAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}