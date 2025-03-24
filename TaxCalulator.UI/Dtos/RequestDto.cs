using static TaxCalulator.UI.Common.AppConstants;

namespace TaxCalulator.UI.Dtos
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; }
        public string Url { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
