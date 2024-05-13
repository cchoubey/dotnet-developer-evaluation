using dotnet_developer_evaluation;
using dotnet_developer_evaluation.Models;
using dotnet_developer_evaluation.Repositories;
using Moq;
using System.Text;

namespace dotnet_developer_evaluation_test
{
    public class CompanyRepositoryTest
    {
        [Fact]
        public async void Returns_404_When_id_not_found()
        {
            var service = new Mock<IService>();
            service.Setup(d => d.GetCompanyById(It.IsAny<int>())).Returns(Task.FromResult<string?>(null));
            service.Setup(d => d.statusCode).Returns(System.Net.HttpStatusCode.NotFound);

            var companyRepository = new CompanyRepository(service.Object);

            var value = await companyRepository.GetById(4);
            Type obj = typeof(Error);
            Assert.IsType(obj, value.Item1);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, value.Item2);

        }

        [Fact]
        public async void Returns_OK_When_id_found()
        {
            //setup
            var service = new Mock<IService>();
            var company = new Company
            {
                Id = 1,
                Name = "Test",
                Description = "Test"
            };
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(company.GetType());
            var memoryStream = new MemoryStream();
            xmlSerializer.Serialize(memoryStream, company);

            memoryStream.Position = 0;
            var reader = new StreamReader(memoryStream, Encoding.UTF8);

            service.Setup(d => d.GetCompanyById(It.IsAny<int>())).Returns(Task.FromResult<string?>(reader.ReadToEnd()));
            service.Setup(d => d.statusCode).Returns(System.Net.HttpStatusCode.OK);
            var companyRepository = new CompanyRepository(service.Object);

            // run
            var value = await companyRepository.GetById(1);

            // test
            Assert.NotNull(value.Item1);
            Type obj = typeof(Company);
            Assert.IsType(obj, value.Item1);
            Assert.Equal(System.Net.HttpStatusCode.OK, value.Item2);

        }

        [Fact]
        public async void Returns_Problem_When_Server_Error()
        {
            //setup
            var service = new Mock<IService>();

            service.Setup(d => d.GetCompanyById(It.IsAny<int>())).Returns(Task.FromResult<string?>(null));
            service.Setup(d => d.statusCode).Returns(System.Net.HttpStatusCode.InternalServerError);
            var companyRepository = new CompanyRepository(service.Object);

            // run
            var value = await companyRepository.GetById(1);

            // test
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, value.Item2);

        }
    }
}