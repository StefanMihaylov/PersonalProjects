namespace ChatClient.Services
{
    using System;
    using System.ServiceModel;

    public abstract class BaseWebService
    {
        public BaseWebService(string serviceSystem, string serviceName, string systemAbbreviation)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                this.ServiceUrl = serviceSystem;
            }
            else
            {
                serviceSystem = serviceSystem.TrimEnd('/');
                serviceName = serviceName.TrimStart('/');
                this.ServiceUrl = string.Format("{0}/{1}", serviceSystem, serviceName);
            }

            this.SystemAbbreviation = systemAbbreviation;
        }

        public string ModelTimeStamp
        {
            get
            {
                return DateTime.Now.ToString("yyyyMMddHHmmssfff");
            }
        }

        public string ModelRequestId
        {
            get
            {
                var now = DateTime.Now;

                int dayOfWeek = (int)now.DayOfWeek;
                dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;

                var timeStamp = now.ToString("HHmmssfff");

                return string.Format("{0}{1}{2}", this.SystemAbbreviation, dayOfWeek, timeStamp);
            }
        }

        protected string ServiceUrl { get; private set; }

        protected string SystemAbbreviation { get; private set; }

        protected BasicHttpBinding Binding
        {
            get
            {
                var binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = int.MaxValue;
                binding.MaxBufferSize = int.MaxValue;
                return binding;
            }
        }

        protected EndpointAddress EndpointAddress
        {
            get
            {
                return new EndpointAddress(this.ServiceUrl);
            }
        }
    }
}
