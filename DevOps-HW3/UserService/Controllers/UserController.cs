using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("info")]
        public IActionResult GetUserInfo()
        {
            return Ok(new { Name = "Sahib Shakhayev", Role = "Admin" });
        }
    }

}
