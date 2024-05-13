using System.Net;

namespace dotnet_developer_evaluation
{
    public interface IService
    {
        HttpStatusCode statusCode { get; }
        string reasonPhrase { get; }

        Task<string?> GetCompanyById(int id);
    }
}