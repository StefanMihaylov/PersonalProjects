using System.Linq;
using System.Web;
using Convertors;
using Convertors.Models;
using Convertors.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests.Convertors
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void GetQueryStringTest()
        {
            var model = new RequestModel()
            {
                FirstName = "John",
                LastName = "Smith",
                Amount = 24,
                Currency = "USD",
                CreditCardNumner = "4444333322221111",
                Cvv = "123",
                IPAddress = string.Empty,
                ExpirationMonth = 3.ToString("d2"),
            };

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CustomSettingsResolver(new CustomSettings
                {
                    IgnoreEmptyValues = true,
                    IsCensoringEnabled = true,
                }),
            };

            string output = JsonConvert.SerializeObject(model, settings);

            var jObj = JObject.FromObject(model, JsonSerializer.Create(settings));
            string queryOutput = string.Join("&", jObj.Properties().Select(s => $"{s.Name}={s.Value}"));

            var dictionary = HttpUtility.ParseQueryString(queryOutput);
            var formDictionary = dictionary.AllKeys
                     .Where(p => dictionary[p] != "null")
                     .ToDictionary(p => p, p => dictionary[p]);

            string responseAsJson = JsonConvert.SerializeObject(formDictionary);
            var respObj = JsonConvert.DeserializeObject<RequestModel>(responseAsJson);

            Assert.IsTrue(output.Length > 1);
        }
    }
}
