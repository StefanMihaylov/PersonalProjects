using Convertors.Models;
using Newtonsoft.Json;

namespace Convertors.Tests.Models
{
    public class RequestModel
    {
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [DisableAuditing(Pattern = Pattern.CreditCard)]
        [JsonProperty(PropertyName = "credit_card")]
        public string CreditCardNumner { get; set; }

        [DisableAuditing]
        [JsonProperty(PropertyName = "cvv2")]
        public string Cvv { get; set; }

        [JsonProperty(PropertyName = "ipaddress")]
        public string IPAddress { get; set; }

        [JsonProperty(PropertyName = "expiration_month")]
        public string ExpirationMonth { get; set; }
    }
}
