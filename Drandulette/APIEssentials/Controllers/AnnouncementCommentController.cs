using Drandulette.Controllers.Data.Models;
using Drandulette.Controllers.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drandulette.APIEssentials.Controllers
{
    [Route("announcement/[controller]")]
    [ApiController]
    public class AnnouncmentCommentController : ControllerBase
    {
        private readonly DranduletteContext dbConnector;

        public AnnouncmentCommentController(DranduletteContext dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        [HttpPost(Name = "PostAnnouncmentComment")]
        public IActionResult Post([FromBody] Announcement_comment announcement_Comment)
        {
            try
            {
                announcement_Comment.announcment_commentID = Guid.NewGuid().ToString();
                announcement_Comment.time = DateTime.Now;

                dbConnector.Announcement_comment.Add(announcement_Comment);
                dbConnector.SaveChanges();
            }
            catch { return BadRequest(); }

            return Ok();
        }

        [HttpGet(Name = "GetAnnouncmentComment")]
        public IEnumerable<Announcement_comment> Get()
        {
            return dbConnector.Announcement_comment.Select(comment => comment);            
        }

        [HttpDelete(Name = "DeleteAnnouncmentComment")]
        public void Delete(string commentID)
        {
            var commentToDelete = dbConnector.Announcement_comment.Find(commentID);

            if (commentToDelete != null)
            {
                dbConnector.Announcement_comment.Remove(commentToDelete);
                dbConnector.SaveChanges();
            }
        }
    }
}
