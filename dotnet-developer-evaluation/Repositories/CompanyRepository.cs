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

            if (response != null) 
            {
                var serializer = new XmlSerializer(typeof(Company));
                var company = (Company?)serializer.Deserialize(response);
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
