namespace ChatClient.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Services.ChatRoomsService;
    using Services.MessagesService;
    using Services.ParticipantsService;
    using Duplex = Services.DuplexService;
    using System.ServiceModel;

    public class HomeController : Controller
    {
        private readonly ParticipantsManagerClient m_ParticipantsService;
        private readonly ChatRoomsManagerClient m_ChatRoomsService;
        private readonly MessagesManagerClient m_MessagesService;
       // private readonly Duplex.DuplexServiceClient m_DuplexService;

        public HomeController()
        {
            m_ParticipantsService = new ParticipantsManagerClient();
            m_ChatRoomsService = new ChatRoomsManagerClient();
            m_MessagesService = new MessagesManagerClient();

            //var callback = new Duplex.DuplexServiceCallback();
            //var instanceContext = new InstanceContext(callback);
            //m_DuplexService = new Duplex.DuplexServiceClient(instanceContext);
        }

        protected override void Dispose(bool disposing)
        {
            m_ParticipantsService.Close();
            m_ChatRoomsService.Close();
            m_MessagesService.Close();
            // m_DuplexService.Close();

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Login(string username)
        {
            Participant loggedParticipant = this.m_ParticipantsService.Login(username);

           // m_DuplexService.GetAllOnline(username);

            return this.PartialView("_Login", loggedParticipant);
        }

        public ActionResult Logout(string username)
        {
            this.m_ParticipantsService.Logout(username);
            return this.RedirectToAction("Index");
        }

        public ActionResult Contacts(string username)
        {
            IEnumerable<Participant> otherOnlineUsers = this.m_ParticipantsService.GetAllOnline(username);
            return this.PartialView("_Contacts", otherOnlineUsers);
        }

        public ActionResult OpenRoom(IEnumerable<string> usernames)
        {
            ChatRoom model = m_ChatRoomsService.OpenChatRoom(usernames.ToArray());
            return this.PartialView("_ChatRoom", model);
        }

        public ActionResult OpenRoomById(int chatRoomId)
        {
            ChatRoom model = m_ChatRoomsService.OpenChatRoomById(chatRoomId);
            return this.PartialView("_ChatRoom", model);
        }

        public ActionResult CloseRoom(int chatRoomId)
        {
            m_ChatRoomsService.CloseChatRoomById(chatRoomId);
            return this.Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllOpenChatRooms(string username)
        {
            IEnumerable<ChatRoom> chatRooms = m_ChatRoomsService.GetAllOpenChatRooms(username);
            return this.PartialView("_OpenChatRooms", chatRooms);
        }

        public ActionResult SendMessage(MessageInput input)
        {
            m_MessagesService.AddMessage(input);
            IEnumerable<Message> messages = m_MessagesService.GetMessages(input.ChatRoomId);
            return this.PartialView("_Messages", messages);
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
                model = m_MessagesService.GetMessages(roomId.Value);
            }

            return this.PartialView("_Messages", model);
        }
    }
}