namespace ChatClient.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ChatClient.Services.Chat;

    public class ChatService : BaseWebService
    {
        public const string ServiceName = "ChatService.svc";

        private ChatServiceClient serviceClient;

        public ChatService()
            : base("http://localhost/chatserver/", ServiceName, string.Empty)
        {
            this.serviceClient = new ChatServiceClient(this.Binding, this.EndpointAddress);
        }

        public Participant Login(string username)
        {
            Participant user = this.serviceClient.Login(username);
            return user;
        }


    }
}
