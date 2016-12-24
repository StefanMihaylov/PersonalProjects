namespace ChatClient.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using ChatClient.Services.Chat;

    public class LoginModel
    {
        public Participant CurrentUser { get; set; }

        public IEnumerable<Participant> OtherUsers { get; set; }
    }
}