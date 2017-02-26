namespace ChatClient.Services.DuplexService
{
    public class DuplexServiceCallback : IDuplexServiceCallback
    {
        public void GetAllOnlineCallBack(Participant[] users)
        {
            var count = users.Length;
        }
    }
}
