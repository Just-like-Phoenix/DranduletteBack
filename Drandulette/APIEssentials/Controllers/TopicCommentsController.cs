using Drandulette.Controllers.Data;
using Drandulette.Controllers.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Drandulette.APIEssentials.Controllers
{
    [Route("topic/[controller]")]
    [ApiController]
    public class TopicCommentsController : ControllerBase
    {
        private readonly DranduletteContext dbConnector;

        public TopicCommentsController(DranduletteContext dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        [HttpPost(Name = "PostTopicComment")]
        public IActionResult Post([FromBody] Topic_comment topic_Comment)
        {
            try
            {
                topic_Comment.topicID = Guid.NewGuid().ToString();

                DateTime time = DateTime.Now;

                topic_Comment.time = $"{time.Day}.{time.Month}.{time.Year} {time.Hour}:{time.Minute}:{time.Second}";

                dbConnector.Topic_comment.Add(topic_Comment);
                dbConnector.SaveChanges();
            }
            catch { return BadRequest(); }

            return Ok();
        }

        [HttpGet(Name = "GetTopicComment")]
        public IEnumerable<Topic_comment>? Get(string topicID, string? probableMessage)
        {
            try
            {
                if (probableMessage == null) probableMessage = string.Empty;

                IEnumerable<Topic_comment> comments = dbConnector.Topic_comment.Where(comment => comment.message.ToLower().Contains(probableMessage) || comment.topicID == topicID);

                foreach(var comment in comments)
                {
                    comment.user = dbConnector.User.Find(comment.mailLogin);
                }

                return comments;
            }
            catch { return new List<Topic_comment>(); }
        }

        [HttpDelete(Name = "DeleteTopicComment")]
        public void Delete(string topic_commentID)
        {
            var topicCommentToDelete = dbConnector.Topic_comment.Find(topic_commentID);

            if (topicCommentToDelete != null)
            {
                dbConnector.Topic_comment.Remove(topicCommentToDelete);
                dbConnector.SaveChanges();
            }
        }
    }
}
