using System.Net;

namespace TaxCalulator.UI.Dtos
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Error { get; set; }
    }
}
