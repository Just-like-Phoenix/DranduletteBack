using Drandulette.Controllers.Data.Models;
using Drandulette.Controllers.Data;
using Microsoft.AspNetCore.Mvc;
using static Drandulette.APIEssentials.Controllers.GlobalMethods;

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
        public IEnumerable<Announcement_comment> Get(string announcmentID)
        {
            List<Announcement_comment> comments = dbConnector.Announcement_comment.Where(comment => comment.announcmentID == announcmentID).ToList();

            foreach (var comment in comments)
            {
                comment.user = dbConnector.User.Find(comment.mailLogin);
            }

            return comments.Select(x => InsertPictures(x)).OrderBy(x => x.time);
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
