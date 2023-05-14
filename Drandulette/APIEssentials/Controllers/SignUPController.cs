using Drandulette.Controllers.Data;
using Drandulette.Controllers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using FileManager = System.IO.File;

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
                string path = $".\\Users\\{user.mailLogin}";
                Directory.CreateDirectory(path);

                if (user.profilePic == null)
                    FileManager.Copy($".\\Samples\\imgnotfound.base", $"{path}\\imgnotfound.base");
                else
                    FileManager.WriteAllText($"{path}\\imgnotfound.base", user.profilePic);
                

                dbContext.User.Add(user);
                dbContext.SaveChanges();
            }
            catch { return BadRequest(); }
            return Ok();
        }

        [HttpDelete(Name = "DeleteSignUP")]
        public IActionResult Delete(string? userMailLogin)
        {
            var user = dbContext.User.Find(userMailLogin);

            if (user != null)
            {
                dbContext.User.Remove(user);

                var announcments = Directory.EnumerateDirectories($".\\Users\\{userMailLogin}");
                var files = Directory.EnumerateFiles($".\\Users\\{userMailLogin}");

                foreach (var dir in announcments)
                {
                    var tmpFiles = Directory.EnumerateFiles(dir);

                    foreach (var file in tmpFiles)
                        FileManager.Delete(file);

                    Directory.Delete(dir);
                }

                foreach (var file in files)
                    FileManager.Delete(file);

                Directory.Delete($".\\Users\\{userMailLogin}");

                dbContext.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}
