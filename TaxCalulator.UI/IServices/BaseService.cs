using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using TaxCalulator.UI.Common;
using TaxCalulator.UI.Dtos;
using TaxCalulator.UI.IServices.Interface;
using TaxCalulator.UI.Dtos.Wrappers;

namespace TaxCalulator.UI.IServices
{
    public class BaseService(IHttpClientFactory clientFactory) : IBaseService
    {
        private readonly IHttpClientFactory _clientFactory = clientFactory;
        
        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            //1. Create http client from the factory
            var client = _clientFactory.CreateClient();

            var headers = client.DefaultRequestHeaders;
            headers.Add("Accept", "application/json");            

            //2.  Instantiate HttpRequestMessage and move data to it from dto

            var httpReqMessage = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json"),
                RequestUri = new Uri(requestDto.Url),    
                
            };

            httpReqMessage.Method = requestDto.ApiType switch
            {
                AppConstants.ApiType.GET => HttpMethod.Get,
                AppConstants.ApiType.POST => HttpMethod.Post,
                AppConstants.ApiType.PUT => HttpMethod.Put,
                _ => HttpMethod.Delete,
            };

            //3. Call the api
            HttpResponseMessage httpResMessage = await client.SendAsync(httpReqMessage);

            //4. Segregate the httpResMessage as per its status code and handle appropriately.
            
            ResponseDto _responseDto = new();
            
            if (httpResMessage != null)
            {
                JsonSerializerSettings settings = new()
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    
                };

                switch (httpResMessage.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        var response = await httpResMessage.Content.ReadAsStringAsync();
                        _responseDto.Error = JsonConvert.DeserializeObject<string>(response);
                        break;

                    case HttpStatusCode.InternalServerError:
                        var content = await httpResMessage.Content.ReadAsStringAsync();
                        _responseDto.Error = JsonConvert.DeserializeObject<string>(content);
                        break;

                    case HttpStatusCode.NotFound:
                        var output = await httpResMessage.Content.ReadAsStringAsync();
                        _responseDto.Error = JsonConvert.DeserializeObject<string>(output);
                        break;

                    default:
                        var apiResponse = await httpResMessage.Content.ReadAsStringAsync();
                        _responseDto.Result = JsonConvert.DeserializeObject<object>(apiResponse, settings);
                        break;
                }
            }
            return _responseDto!;
        }
    }
}
