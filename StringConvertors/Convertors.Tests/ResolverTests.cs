using Convertors.Models;
using Convertors.NewtonJson.Resolvers;
using Convertors.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Tests.Convertors
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void GetQueryStringTest()
        {
            var model = new RequestModel("John", "Doe", 24, "USD", "4444333322221111", "123");
            var expected = @"{
  ""first_name"": ""John"",
  ""last_name"": ""Doe"",
  ""Amount"": 24.0,
  ""currency"": ""USD"",
  ""credit_card"": ""444433******1111"",
  ""cvv2"": ""***""
}";

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

            Assert.AreEqual(expected, output);
        }
    }
}
