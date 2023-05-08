using Drandulette.Controllers.Data;
using Drandulette.Controllers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using FileManager = System.IO.File;

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
            User? account = null;

            try {
                account = dbContext.User.Single(user => user.mailLogin == login && user.password == password);
                account.profilePic = Convert.ToBase64String(FileManager.ReadAllBytes($".\\Users\\{account.mailLogin}\\imgnotfound.base"));
            }
            catch {}

            return account;
        }
    }
}
