namespace Convertors.Converters
{
    public interface IStringConvertor
    {
        T FromJson<T>(string jsonString);

        T FromQueryString<T>(string queryString);

        T FromXmlString<T>(string xmlString, bool omitRootObject);

        string ToJson(object value, bool censored = true);

        string ToQueryString(object value, bool censored = true);

        string ToXmlString(object value, string rootElementName, bool censored = true);
    }
}