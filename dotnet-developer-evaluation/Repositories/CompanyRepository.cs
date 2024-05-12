using dotnet_developer_evaluation.Models;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace dotnet_developer_evaluation.Repositories
{
    public class CompanyRepository
    {
        //private readonly string _baseUrl;
        private readonly IService _service;
        public CompanyRepository(IConfiguration configuration)
        {
            var baseUrl = configuration.GetValue<string>("baseUrl") ?? "";
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            _service = new Service(client);
        }

        public async Task<(Company?, HttpStatusCode)> GetById(int id)
        {
            var response = await _service.GetCompanyById(id);

            if (!string.IsNullOrWhiteSpace(response)) 
            {
                var serializer = new XmlSerializer(typeof(Company));
                var byteArray = Encoding.UTF8.GetBytes(response);
                var stream = new MemoryStream(byteArray);
                var company = (Company?)serializer.Deserialize(stream);
                return (company, _service.statusCode);
            }
            return (null, _service.statusCode);
        }
    }
}
