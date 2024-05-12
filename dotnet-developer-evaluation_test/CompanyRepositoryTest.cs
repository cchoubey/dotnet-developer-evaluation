using dotnet_developer_evaluation;
using dotnet_developer_evaluation.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;

namespace dotnet_developer_evaluation_test
{
    public class CompanyRepositoryTest
    {
        private CompanyRepository setup()
        {
            var baseUrl = "https://raw.githubusercontent.com/openpolytechnic/dotnet-developer-evaluation/main/xml-api/";
            var inMemorySettings = new Dictionary<string, string> {
                    {"baseUrl", baseUrl}};

            IConfiguration Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                 .Build();
            return new CompanyRepository(Configuration);
        }
        [Fact]
        public async void Returns_404_When_id_not_found()
        {
            var companyRepository = setup();

            var value = await companyRepository.GetById(4);

            Assert.Null(value.Item1);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, value.Item2);

        }

        [Fact]
        public async void Returns_OK_When_id_found()
        {
            var companyRepository = setup();

            var value = await companyRepository.GetById(1);

            Assert.NotNull(value.Item1);
            Assert.Equal(System.Net.HttpStatusCode.OK, value.Item2);

        }
    }
}