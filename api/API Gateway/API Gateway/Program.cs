using Microsoft.OpenApi.Models;
using Grpc.Net.Client;
using DataWorker;

var builder = WebApplication.CreateBuilder(args);

string version = "v1";

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(version, new OpenApiInfo
    {
        Title = "CustomerDB API",
        Version = version,
        Description = "Das ist das API Gateway der Datenbank CustomerDB"
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/" + version + "/swagger.json", "CustomerDB API" + version);
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
