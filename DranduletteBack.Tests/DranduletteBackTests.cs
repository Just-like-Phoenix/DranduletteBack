using Drandulette.APIEssentials.Controllers;
using Drandulette.Controllers.Data;
using Drandulette.Controllers.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DranduletteBack.Tests
{
    public class DranduletteBackTests
    {
        private static User testUser = new User
        {
            mailLogin = "test@mail.ru",
            password = "test",
            moderator = 0,
            name = "Vadim",
            phone = "+375296662340",
            profilePic = null
        };
        private static List<Topic> testTopics = new List<Topic>(Enumerable.Range(1, 1)
                                                   .Select(topic => new Topic
                                                   {
                                                       mailLogin = testUser.mailLogin,
                                                       topic_text = new string(Enumerable.Range(1, 100)
                                                                                         .Select(x => 'a')
                                                                                         .ToArray()),
                                                       topic_theme = new string(Enumerable.Range(1, 10)
                                                                                          .Select(x => 't')
                                                                                          .ToArray()),
                                                       topicID = "11111111"
                                                   }));
        private static Topic_comment comment = new Topic_comment
        {
            mailLogin = testUser.mailLogin,
            message = "test message",
            topic_commentID = "11111111",
            topicID = "11111111"
        };
        private static Announcement announcement = new Announcement
        {
            announcementID = "11111111",
            body = "test",
            brand = "test",
            fuelType = "test",
            hp = 1,
            mailLogin = testUser.mailLogin,
            mileage = 1,
            model = "test",
            picsPath = "D:\\Cars\\test",
            price = 1,
            sellersComment = "test",
            transmission = "test",
            volume = 1,
            wheelDrive = "test",
            year = 1,
            pics = Directory.EnumerateFiles("D:\\Cars\\test").ToList()
        };
        private static Announcement_comment announcement_Comment = new Announcement_comment
        {
            announcmentID = announcement.announcementID,
            announcment_commentID = "11111111",
            mailLogin = testUser.mailLogin,
            message = "test message"
        };

        private DbContextOptions<DranduletteContext> getDbOptions()
        {
            string connectionString = "server=localhost;port=3306;database=drandulette;user=root;password=root";
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            return (new DbContextOptionsBuilder<DranduletteContext>().UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure())
                   .LogTo(Console.WriteLine, LogLevel.Information)
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors()).Options;
        }

        [Fact]
        public void A_SignUPController_Post_ReturnStatusCode()
        {
            var testSignUP = new SignUPController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testSignUP.Post(testUser).ToString());
        }

        [Fact]
        public void B_LogINController_Get_ReturnStatusCode()
        {
            var testLogIN = new LogINController(new DranduletteContext(getDbOptions()));
            var userFromDb = testLogIN.Get(testUser.mailLogin, testUser.password);

            Assert.Equal(testUser.mailLogin, userFromDb.mailLogin);
            Assert.Equal(testUser.password, userFromDb.password);
        }

        [Fact]
        private void C_TopicController_Post_ReturnStatusCode()
        {
            var testTopicController = new TopicController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testTopicController.Post(testTopics[0]).ToString());
        }

        [Fact]
        public void D_TopicController_Get_ReturnStatusCode()
        {
            var testTopicController = new TopicController(new DranduletteContext(getDbOptions()));
            var testedTopicList = testTopicController.Get(testTopics[0].topicID, "", 0).ToList();

            Assert.Equal(testTopics[0].topicID, testedTopicList[0].topicID);
        }

        [Fact]
        public void E_TopicCommentController_Post_ReturnStatusCode()
        {
            var testTopicCommentController = new TopicCommentsController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testTopicCommentController.Post(comment).ToString());
        }

        [Fact]
        public void F_TopicCommentController_Get_ReturnStatusCode()
        {
            var testTopicCommentController = new TopicCommentsController(new DranduletteContext(getDbOptions()));

            Assert.Equal(comment.message, testTopicCommentController.Get(comment.topicID).ToList()[0].message);
        }

        [Fact]
        public void G_TopicCommentController_Delete_ReturnStatusCode()
        {
            var testTopicCommentController = new TopicCommentsController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testTopicCommentController.Delete(comment.topic_commentID).ToString());
        }

        [Fact]
        public void H_TopicController_Delete_ReturnStatusCode()
        {
            var testTopicController = new TopicController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testTopicController.Delete(testTopics[0].topicID).ToString());
        }

        [Fact]
        public void I_AnnouncmentController_Post_ReturnStatusCode()
        {
            var testAnnouncmentController = new AnnouncementController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testAnnouncmentController.Post(announcement).ToString());
        }

        [Fact]
        public void J_AnnouncmentController_Get_ReturnStatusCode()
        {
            var testAnnouncmentController = new AnnouncementController(new DranduletteContext(getDbOptions()));

            Assert.Equal(announcement.announcementID, testAnnouncmentController.Get(announcement.announcementID, null, null, 0, 1, 0).ToList()[0].announcementID);
        }

        [Fact]
        public void K_AnnouncmentCommentController_Post_ReturnStatusCode()
        {
            var testAnnouncmentCommentController = new AnnouncmentCommentController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testAnnouncmentCommentController.Post(announcement_Comment).ToString());
        }

        [Fact]
        public void L_AnnouncmentCommentController_Get_ReturnStatusCode()
        {
            var testAnnouncmentCommentController = new AnnouncmentCommentController(new DranduletteContext(getDbOptions()));

            Assert.Equal(announcement_Comment.announcment_commentID, testAnnouncmentCommentController.Get(announcement.announcementID)
                                                                                                     .ToList()
                                                                                                     .Single(x => x.announcmentID == announcement.announcementID)
                                                                                                     .announcment_commentID);
        }

        [Fact]
        public void O_AnnouncmentCommentController_Delete_ReturnStatusCode()
        {
            var testAnnouncmentCommentController = new AnnouncmentCommentController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testAnnouncmentCommentController.Delete(announcement_Comment.announcment_commentID).ToString());
        }

        [Fact]
        public void P_AnnouncmentController_Delete_ReturnStatusCode()
        {
            var testAnnouncmentController = new AnnouncementController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testAnnouncmentController.Delete(announcement.announcementID).ToString());
        }

        [Fact]
        public void Q_SignUPController_Delete_ReturnStatusCode()
        {
            var testSignUP = new SignUPController(new DranduletteContext(getDbOptions()));

            Assert.Equal(new OkResult().ToString(), testSignUP.Delete(testUser.mailLogin).ToString());
        }
    }
}