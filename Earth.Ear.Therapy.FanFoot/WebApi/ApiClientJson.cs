using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace Earth.Ear.Therapy.FanFoot.WebApi
{
    public class ApiClientJson : ApiClient
    {
        public ApiClientJson(string restApiBaseUrl) : base(restApiBaseUrl, new JsonMediaTypeFormatter())
        {
        }

        protected override HttpContent CreateContent<TRequestDto>(TRequestDto requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
