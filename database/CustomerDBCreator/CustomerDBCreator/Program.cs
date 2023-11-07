using CustomerDBCreator.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var cc = scope.ServiceProvider.GetRequiredService<CustomerContext>();
    cc.Database.EnsureCreated();
}