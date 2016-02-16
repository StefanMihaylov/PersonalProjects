namespace TweeterBackup.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class Extensions
    {
        public static string ToWebString(this SortedDictionary<string, string> source)
        {
            var body = new StringBuilder();
            foreach (var requestParameter in source)
            {
                body.Append(requestParameter.Key);
                body.Append("=");
                body.Append(Uri.EscapeDataString(requestParameter.Value));
                body.Append("&");
            }

            if (body.Length > 0)
            {
                // remove trailing '&' 
                body.Remove(body.Length - 1, 1);
            }

            return body.ToString();
        }

        public static string PopulateTweetLinks(this string tweetText)
        {
            string regexHtHyperLink = @"(http|ftp|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?";
            var urlRx = new Regex(regexHtHyperLink, RegexOptions.IgnoreCase);
            MatchCollection matches = urlRx.Matches(tweetText);

            return matches.Cast<Match>()
                          .Aggregate(tweetText, (current, match) => current.Replace(match.Value, string.Format("({1})", match.Value, match.Value)));
        }
    }
}
