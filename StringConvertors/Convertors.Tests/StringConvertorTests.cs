using Convertors.NewtonJson.Converters;
using Convertors.Tests.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Convertors.Tests
{
    [TestClass]
    public class StringConvertorTests
    {
        [TestMethod]
        public void ReturnUncensoredFullModelFormated()
        {
            var model = new RequestModel("John", "Doe", 24, "USD", "4444333322221111", "123");
            var expected = @"{
  ""first_name"": ""John"",
  ""last_name"": ""Doe"",
  ""Amount"": 24.0,
  ""currency"": ""USD"",
  ""credit_card"": ""4444333322221111"",
  ""cvv2"": ""123""
}";

            var converter = new StringConvertor(null, null);

            var result = converter.ToJson(model, false);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnUncensoredFullModel()
        {
            var model = new RequestModel("John", "Doe", 24, "USD", "4444333322221111", "123");
            var expected = "{\"first_name\":\"John\",\"last_name\":\"Doe\",\"Amount\":24.0,\"currency\":\"USD\",\"credit_card\":\"4444333322221111\",\"cvv2\":\"123\"}";

            var setings = new JsonSerializerSettings() {  Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
            var converter = new StringConvertor(Options.Create(setings), null);
            
            var result = converter.ToJson(model, false);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnCensoredFullModel()
        {
            var model = new RequestModel("John", "Doe", 24, "USD", "4444333322221111", "123");
            var expected = "{\"first_name\":\"John\",\"last_name\":\"Doe\",\"Amount\":24.0,\"currency\":\"USD\",\"credit_card\":\"444433******1111\",\"cvv2\":\"***\"}";

            var setings = new JsonSerializerSettings() { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
            var converter = new StringConvertor(Options.Create(setings), null);
            
            var result = converter.ToJson(model, true);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnCensoredFullModelMissingProperties()
        {
            var model = new RequestModel("John", null, 24, "USD", "4444333322221111", "123");
            var expected = "{\"first_name\":\"John\",\"Amount\":24.0,\"currency\":\"USD\",\"credit_card\":\"444433******1111\",\"cvv2\":\"***\"}";

            var setings = new JsonSerializerSettings() { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
            var converter = new StringConvertor(Options.Create(setings), null);

            var result = converter.ToJson(model, true);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnQueryUncensoredFullModel()
        {
            var model = new RequestModel("John", "Doe", 24, "USD", "4444333322221111", "123");
            var expected = "first_name=John&last_name=Doe&Amount=24&currency=USD&credit_card=4444333322221111&cvv2=123";

            var setings = new JsonSerializerSettings() { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
            var converter = new StringConvertor(Options.Create(setings), null);

            var result = converter.ToQueryString(model, false);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnQueryCensoredFullModel()
        {
            var model = new RequestModel("John", "Doe", 24, "USD", "4444333322221111", "123");
            var expected = "first_name=John&last_name=Doe&Amount=24&currency=USD&credit_card=444433******1111&cvv2=***";

            var setings = new JsonSerializerSettings() { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
            var converter = new StringConvertor(Options.Create(setings), null);

            var result = converter.ToQueryString(model, true);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnQueryCensoredMissingProperties()
        {
            var model = new RequestModel("John", null, 24, "USD", "4444333322221111", "123");
            var expected = "first_name=John&Amount=24&currency=USD&credit_card=444433******1111&cvv2=***";

            var setings = new JsonSerializerSettings() { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
            var converter = new StringConvertor(Options.Create(setings), null);

            var result = converter.ToQueryString(model, true);

            Assert.AreEqual(expected, result);
        }
    }
}
