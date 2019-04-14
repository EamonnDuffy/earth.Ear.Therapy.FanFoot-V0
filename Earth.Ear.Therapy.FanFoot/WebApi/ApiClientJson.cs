using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Earth.Ear.Ot.FantasyFootball.WebApi
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
