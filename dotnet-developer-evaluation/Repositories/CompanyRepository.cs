using dotnet_developer_evaluation.Models;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Serialization;

namespace dotnet_developer_evaluation.Repositories
{
    public class CompanyRepository
    {
        private readonly IService _service;
        public CompanyRepository(IService service)
        {
            _service = service;
        }

        public async Task<(object?, HttpStatusCode)> GetById(int id)
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
            else
            {
                var errorObj = new Error
                {
                    error = _service.statusCode.ToString(),
                    error_description = _service.reasonPhrase
                };
                return (errorObj, _service.statusCode);
            }
           
        }
    }
}
