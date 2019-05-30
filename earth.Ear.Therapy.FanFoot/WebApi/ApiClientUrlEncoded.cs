using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Earth.Ear.Therapy.FanFoot.WebApi
{
    public class ApiClientUrlEncoded : ApiClient
    {
        public ApiClientUrlEncoded(string restApiBaseUrl) : base(restApiBaseUrl, new FormUrlEncodedMediaTypeFormatter())
        {
        }

        protected override HttpContent CreateContent<TRequestDto>(TRequestDto requestDto)
        {
            var enumerable = requestDto as IEnumerable<KeyValuePair<string, string>>;

            if (enumerable == null)
            {
                throw new NotSupportedException();
            }

            return new FormUrlEncodedContent(enumerable);
        }
    }
}
