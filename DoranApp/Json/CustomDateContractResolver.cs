using Newtonsoft.Json.Serialization;
using System;

namespace DoranApp.Json
{
    internal class CustomDateContractResolver : DefaultContractResolver
    {
        private readonly string _dateTimeFormat;

        public CustomDateContractResolver(string dateTimeFormat)
        {
            _dateTimeFormat = dateTimeFormat;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            if (objectType == typeof(DateTime))
            {
                contract.Converter = new ZerosIsoDateTimeConverter(_dateTimeFormat, "0000-00-00 00:00:00");
            }

            return contract;
        }
    }
}
