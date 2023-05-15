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
                account.profilePic = FileManager.ReadAllText($".\\Users\\{account.mailLogin}\\imgnotfound.base");
            }
            catch {}

            return account;
        }

        [HttpPut(Name = "UpdUser")]
        public IActionResult Update(string mailLogin, int verification, int banned)
        {
            try
            {
                var userToUpdate = dbContext.User.Find(mailLogin);

                userToUpdate.verificated = verification;
                userToUpdate.banned = banned;

                dbContext.Update(userToUpdate);
                dbContext.SaveChanges();

                return Ok();
            }
            catch { return BadRequest(); }
        }
    }
}
