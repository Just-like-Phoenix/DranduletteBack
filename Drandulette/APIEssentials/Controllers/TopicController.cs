using Drandulette.Controllers.Data;
using Drandulette.Controllers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using static Drandulette.APIEssentials.Controllers.GlobalMethods;


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
        public IEnumerable<Topic> Get(string? topicID, string? probableName, int page)
        {
            try
            {
                if (probableName == null) probableName = string.Empty;
                if (topicID == null) topicID = string.Empty;

                int count = dbConnector.Topic.Count();
                int start = page * 6;
                int end = start > count - 6 ? count : start + 6;

                List<Topic> topics = dbConnector.Topic
                                        .Where(x => (x.topic_theme.Contains(probableName) || x.topic_text.Contains(probableName)) && x.topicID.Contains(topicID))
                                        .ToList();

                foreach (Topic topic in topics)
                {
                    topic.user = dbConnector.User.Find(topic.mailLogin);
                }

                return topics.Select(x => InsertPictures(x)).Take(new Range(start, end));
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
