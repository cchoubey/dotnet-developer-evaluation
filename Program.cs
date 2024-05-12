using dotnet_developer_evaluation.ApiExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.UseCompaniesApi();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Api to get Company", Version = "v1" });
});
var app = builder.Build();

app.MapCompaniesApi();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.Run();
