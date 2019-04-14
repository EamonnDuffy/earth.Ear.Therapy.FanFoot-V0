using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Earth.Ear.Ot.FantasyFootball.WebApi
{
    public interface IApiClient : IDisposable
    {
        Task<IApiClientResponse<TResponseDto>> PostAsync<TRequestDto, TResponseDto>(string relativeUrl, TRequestDto requestDto, ApiHeaders optionalHeaders);

        Task<IApiClientResponse<TResponseDto>> GetAsync<TResponseDto>(string relativeUrl, ApiHeaders optionalHeaders);
    }

    public abstract class ApiClient : IApiClient
    {
        private string RestApiBaseUrl { get; }

        private MediaTypeFormatter MediaTypeFormatter { get; }

        protected ApiClient(string restApiBaseUrl, MediaTypeFormatter mediaTypeFormatter)
        {
            RestApiBaseUrl = restApiBaseUrl;
            MediaTypeFormatter = mediaTypeFormatter;
        }

        public void Dispose()
        {
        }

        protected abstract HttpContent CreateContent<TRequestDto>(TRequestDto requestDto);

        private string UrlCombine(string left, string right)
        {
            string url = null;

            if (string.IsNullOrWhiteSpace(right))
            {
                url = left;
            }
            else if (string.IsNullOrWhiteSpace(left))
            {
                url = right;
            }
            else
            {
                url = left.TrimEnd('/') + "/" + right.TrimStart('/');
            }

            return url;
        }

        private void AddHeaders(HttpRequestMessage httpRequest, ApiHeaders optionalHeaders)
        {
            if (optionalHeaders != null)
            {
                foreach (var header in optionalHeaders)
                {
                    try
                    {
                        httpRequest.Headers.Add(header.Key, header.Value);
                    }
                    catch (Exception exception)
                    {
                        // TODO: Log this Exception.
                        httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
            }
        }

        public async Task<IApiClientResponse<TResponseDto>> PostAsync<TRequestDto, TResponseDto>(string relativeUrl, TRequestDto requestDto, ApiHeaders optionalHeaders)
        {
            var response = new ApiClientResponse<TResponseDto>()
            {
            };

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var httpContent = CreateContent(requestDto))
                    {
                        using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, UrlCombine(RestApiBaseUrl, relativeUrl)))
                        {
                            httpRequest.Content = httpContent;

                            AddHeaders(httpRequest, optionalHeaders);

                            using (var httpResponse = await httpClient.SendAsync(httpRequest))
                            {
                                response.HttpStatusCode = (int)httpResponse.StatusCode;
                                response.HttpResponseHeaders = new ApiHeaders(httpResponse.Headers);

                                response.ResponseText = await httpResponse.Content.ReadAsStringAsync();

                                if (typeof(TResponseDto) != typeof(string))
                                {
                                    response.ResponseDto = await httpResponse.Content.ReadAsAsync<TResponseDto>();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<IApiClientResponse<TResponseDto>> GetAsync<TResponseDto>(string relativeUrl, ApiHeaders optionalHeaders)
        {
            var response = new ApiClientResponse<TResponseDto>()
            {
            };

            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpContent httpContent = null;

                    using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, UrlCombine(RestApiBaseUrl, relativeUrl)))
                    {
                        httpRequest.Content = httpContent;

                        AddHeaders(httpRequest, optionalHeaders);

                        using (var httpResponse = await httpClient.SendAsync(httpRequest))
                        {
                            response.HttpStatusCode = (int)httpResponse.StatusCode;
                            response.HttpResponseHeaders = new ApiHeaders(httpResponse.Headers);

                            response.ResponseText = await httpResponse.Content.ReadAsStringAsync();

                            if (typeof(TResponseDto) != typeof(string))
                            {
                                response.ResponseDto = await httpResponse.Content.ReadAsAsync<TResponseDto>();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                response.HttpStatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
