using Drandulette.Controllers.Data;
using Drandulette.Controllers.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drandulette.APIEssentials.Controllers
{
    [Route("authorization/[controller]")]
    [ApiController]
    public class LogINController : ControllerBase
    {
        private readonly DranduletteContext dbContext;

        public LogINController(DranduletteContext context) => dbContext = context;

        [HttpGet(Name = "GetLogIN")]
        public User? Get(string login, string password)
        {
            User account = null;

            try {
                account = dbContext.User.Single(user => user.mailLogin == login && user.password == password);
            }
            catch {}

            return account;
        }
    }
}
