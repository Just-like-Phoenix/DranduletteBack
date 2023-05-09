using Drandulette.Controllers.Data.Models;
using Drandulette.Controllers.Data;
using Microsoft.AspNetCore.Mvc;
using FileManager = System.IO.File;
using static Drandulette.APIEssentials.Controllers.GlobalMethods;

namespace Drandulette.APIEssentials.Controllers
{
    [Route("announcement/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly DranduletteContext dbConnector;

        public AnnouncementController(DranduletteContext dbConnector)
        {
            this.dbConnector = dbConnector;
        }

        private bool AllPicsAreValid(List<string> pics)
        {
            foreach (var pic in pics)
            {
                var modelInput = new CarsML.ModelInput { ImageSource = Convert.FromBase64String(pic.Split(',')[1]) };
                var result = CarsML.Predict(modelInput);

                if (result.Score.Max() < 0.95) return false;
            }

            return true;
        }

        [HttpPost(Name = "PostAnnouncement")]
        public IActionResult Post([FromBody] Announcement announcement)
        {
            try
            {
                if (!AllPicsAreValid(announcement.pics)) throw new Exception("дебил");

                announcement.announcementID = Guid.NewGuid().ToString();

                string path = $".\\Users\\{announcement.mailLogin}\\{announcement.announcementID}";
                Directory.CreateDirectory(path);

                announcement.picsPath = path;

                dbConnector.Announcement.Add(announcement);

                for (int i = 0; i < announcement.pics.Count; i++)
                    FileManager.WriteAllBytes($"{path}\\{i + 1}.base", announcement.pics[i].Select(x => (byte)x).ToArray());

                dbConnector.SaveChanges();
            }
            catch(Exception ex) { 
                if (ex.Message != "дебил") return BadRequest(); 

                return StatusCode(422);
            }
            
            return Ok();
        }

        [HttpGet(Name = "GetAnnouncement")]
        public IEnumerable<Announcement> Get(string? brand, string? model, int year, int isSpecific)
        {
            if (brand == null) brand = string.Empty;
            if (model == null) model = string.Empty;

            List<Announcement> tmp = dbConnector.Announcement
                .Where(x => (x.brand.Contains(brand) || x.model.Contains(model) || x.year == year))
                .ToList();

            return tmp.Select(x => InsertPictures(x, isSpecific));
        }

        [HttpDelete(Name = "DeleteAnnouncement")]
        public void Delete(string announcementID)
        {
            var announcementToDelete = dbConnector.Topic.Find(announcementID);

            if (announcementToDelete != null)
            {
                dbConnector.Topic.Remove(announcementToDelete);
                dbConnector.SaveChanges();
            }
        }
    }
}
