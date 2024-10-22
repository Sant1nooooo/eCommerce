using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Command_Operations.Products;

namespace Server.API.Products
{
    [Route("api[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ISender _sender;
        public ProductController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromQuery] CreateProduct_Command request, CancellationToken ct = default)
        {
            var result = await _sender.Send(request, ct);
            if (result.IsExisting)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result.Message);
        }
    }
}
