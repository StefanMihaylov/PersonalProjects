namespace ChatClient.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ChatClient.Services;
    using ChatClient.Services.Chat;
    using ChatClient.Web.Models;

    public class HomeController : Controller
    {
        private ChatServiceClient client;

        public HomeController()
        {
            this.client = new ChatServiceClient();
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Login(string username)
        {
            LoginModel model = new LoginModel()
            {
                CurrentUser = this.client.Login(username),
                OtherUsers = this.client.GetOtherPaticipants(username),
            };

            return this.PartialView("_Login", model);
        }

        public ActionResult OpenRoom(string usernameA, string usernameB)
        {
            ChatRoom model = this.client.CreateChatRoom(usernameA, usernameB);
            return this.PartialView("_ChatRoom", model);
        }

        public ActionResult SendMessage(int roomId, int userId, string message)
        {
            this.client.AddMessage(roomId, userId, message);
            IEnumerable<Message> model = this.client.GetAllMessages(roomId);
            return this.PartialView("_Messages", model);
        }

        public ActionResult RefreshMessages(int? roomId)
        {
            IEnumerable<Message> model;
            if (!roomId.HasValue)
            {
                model = new List<Message>();
            }
            else
            {
                model = this.client.GetAllMessages(roomId.Value);
            }
            
            return this.PartialView("_Messages", model);
        }
    }
}