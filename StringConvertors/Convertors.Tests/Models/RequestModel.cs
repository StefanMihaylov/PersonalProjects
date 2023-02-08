using Convertors.Models;
using Newtonsoft.Json;

namespace Convertors.Tests.Models
{
    public class RequestModel
    {
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string? LastName { get; set; }

        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [DisableAuditing(Pattern = Pattern.CreditCard)]
        [JsonProperty(PropertyName = "credit_card")]
        public string CreditCardNumner { get; set; }

        [DisableAuditing]
        [JsonProperty(PropertyName = "cvv2")]
        public string Cvv { get; set; }

        public RequestModel(string firstName, string? lastName, decimal amount, string currency, string creditCardNumner, string cvv)
        {
            FirstName = firstName;
            LastName = lastName;
            Amount = amount;
            Currency = currency;
            CreditCardNumner = creditCardNumner;
            Cvv = cvv;
        }
    }
}
