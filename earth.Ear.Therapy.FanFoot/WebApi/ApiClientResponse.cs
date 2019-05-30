namespace earth.Ear.Therapy.FanFoot.WebApi
{
    public interface IApiClientResponse<out TResponseDto>
    {
        int HttpStatusCode { get; }

        ApiHeaders HttpResponseHeaders { get; }

        string ResponseText { get; }

        TResponseDto ResponseDto { get; }
    }

    public class ApiClientResponse<TResponseDto> : IApiClientResponse<TResponseDto>
    {
        public int HttpStatusCode { get; set; }

        public ApiHeaders HttpResponseHeaders { get; set; }

        public string ResponseText { get; set; }

        public TResponseDto ResponseDto { get; set; }
    }
}
