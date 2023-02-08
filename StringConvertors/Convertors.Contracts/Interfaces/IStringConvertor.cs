namespace Convertors.Interfaces
{
    public interface IStringConvertor
    {
        T? FromJson<T>(string jsonString) where T : class;

        T? FromQueryString<T>(string queryString) where T : class;

        T? FromXmlString<T>(string xmlString, bool omitRootObject) where T : class;

        string ToJson(object value, bool censored = true);

        string ToQueryString(object value, bool censored = true);

        string ToXmlString(object value, string rootElementName, bool censored = true);
    }
}