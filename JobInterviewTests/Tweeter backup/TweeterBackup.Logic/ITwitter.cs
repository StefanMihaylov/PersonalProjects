namespace TweeterBackup.Logic
{
    public interface ITwitter
    {
        string GetMentions(int count);

        string GetTweets(string screenName, int count);

        string GetTweetById(long id);

        string RetweetById(long id);

        string GetUserInfo(string screenName);

        string PostStatusUpdate(string status, double latitude, double longitude);

        string SendDirectMessage(string screenName, string text);
    }
}
