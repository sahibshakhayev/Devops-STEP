using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("list")]
        public IActionResult GetProducts()
        {
            return Ok(new[] { "Apple", "Banana", "Orange" });
        }
    }
}
