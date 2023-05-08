using Drandulette.Controllers.Data;
using Drandulette.Controllers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Drandulette.APIEssentials.Controllers
{
    [Route("topic/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly DranduletteContext dbConnector;

        public TopicController(DranduletteContext dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        [HttpPost(Name = "PostTopic")]
        public IActionResult Post([FromBody] Topic topic)
        {
            try
            {
                topic.topicID = Guid.NewGuid().ToString();

                DateTime time = DateTime.Now;

                topic.time = $"{time.Day}.{time.Month}.{time.Year} {time.Hour}:{time.Minute}:{time.Second}";

                dbConnector.Topic.Add(topic);
                dbConnector.SaveChanges();
            }
            catch { return BadRequest(); }
            return Ok();
        }

        [HttpGet(Name = "GetTopic")]
        public IEnumerable<Topic> Get(string? probableName)
        {
            try
            {
                if (probableName == null) probableName = string.Empty;

                IEnumerable<Topic> topics = dbConnector.Topic.Where(x => x.topic_theme.Contains(probableName) || x.topic_text.Contains(probableName)).ToArray();

                foreach (var topic in topics)
                {
                    topic.user = dbConnector.User.Find(topic.mailLogin);
                }

                return topics;
            }
            catch { return new List<Topic>(); }
        }

        [HttpDelete(Name = "DeleteTopic")]
        public void Delete(string topicID)
        {
            var topicToDelete = dbConnector.Topic.Find(topicID);

            if (topicToDelete != null)
            {
                dbConnector.Topic.Remove(topicToDelete);
                dbConnector.SaveChanges();
            }
        }
    }
}
