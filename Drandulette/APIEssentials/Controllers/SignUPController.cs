using Drandulette.Controllers.Data;
using Drandulette.Controllers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Drandulette.APIEssentials.Controllers
{
    [Route("authorization/[controller]")]
    [ApiController]
    public class SignUPController : ControllerBase
    {
        private readonly DranduletteContext dbContext;

        public SignUPController(DranduletteContext context) => dbContext = context;

        [HttpPost(Name = "PostSingUP")]
        public IActionResult Post([FromBody] User user)
        {
            
            try
            {
                Directory.CreateDirectory($".\\Users\\{user.mailLogin}");

                dbContext.User.Add(user);
                dbContext.SaveChanges();
            }
            catch { return BadRequest(); }
            return Ok();
        }
    }
}
