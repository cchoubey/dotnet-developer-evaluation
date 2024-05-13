using System.Net;

namespace dotnet_developer_evaluation
{
    public interface IService
    {
        HttpStatusCode statusCode { get; }
        string reasonPhrase { get; }

        Task<Stream> GetCompanyById(int id);
    }
}