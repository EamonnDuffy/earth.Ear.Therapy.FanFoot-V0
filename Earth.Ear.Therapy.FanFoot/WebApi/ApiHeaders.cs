using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Earth.Ear.Ot.FantasyFootball.WebApi
{
    public class ApiHeaders : List<KeyValuePair<string, string>>
    {
        public ApiHeaders()
        {
        }

        public ApiHeaders(HttpHeaders httpHeaders)
        {
            foreach (var httpHeader in httpHeaders)
            {
                foreach (var headerValue in httpHeader.Value)
                {
                    Add(httpHeader.Key, headerValue);
                }
            }
        }

        public void Add(string key, string value)
        {
            Add(new KeyValuePair<string, string>(key, value));
        }

        public string GetLastMatching(string headerName)
        {
            string headerValue = null;

            foreach (var headerPair in this)
            {
                if (string.Compare(headerPair.Key, headerName, true) == 0)
                    headerValue = headerPair.Value;
            }

            return headerValue;
        }
    }
}