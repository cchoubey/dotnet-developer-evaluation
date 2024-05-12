using System.Net;

namespace dotnet_developer_evaluation
{
    public class Service : IService
    {
        public HttpStatusCode statusCode { get; private set; }
        private readonly HttpClient _httpClient;
        public Service(IConfiguration configuration)
        {
            var baseUrl = configuration.GetValue<string>("baseUrl") ?? "";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task<string?> GetCompanyById(int id)
        {
            string? responseData = null;
            var response = await _httpClient.GetAsync($"{id}.xml");
            statusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                responseData = await response.Content.ReadAsStringAsync();
            }
            return responseData;
        }
    }
}
