using dotnet_developer_evaluation.Models;
using dotnet_developer_evaluation.Repositories;
using System.Net;

namespace dotnet_developer_evaluation.ApiExtensions
{
    public static class CompanyApiExtensions
    {
        public static IServiceCollection UseCompaniesApi(this IServiceCollection services)
        {
            services.AddTransient<CompanyRepository>();

            return services;
        }
        public  static WebApplication MapCompaniesApi(this WebApplication app)
        {
            app.MapGet("/companies/{id:int}", async(int id, CompanyRepository repository) =>
            {

                (Company?, HttpStatusCode) value = await repository.GetById(id);
                if (value.Item1 == null)
                {
                    if (value.Item2 == HttpStatusCode.NotFound)
                    {
                        return Results.NotFound();
                    }
                    return Results.Problem(null, null, (int)value.Item2, value.Item2.ToString(), null, null);
                }
                return Results.Ok(value.Item1);
            });

            return app;

        }
    }
}
