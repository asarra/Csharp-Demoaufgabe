using DataWorker.Models;
using DataWorker.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddDbContext<CustomerContext>();

var app = builder.Build();
app.MapGrpcService<WorkerService>();

app.MapGet("/", () => "Hello World!");

app.Run();
