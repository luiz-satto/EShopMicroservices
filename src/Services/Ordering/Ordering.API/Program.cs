using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();
#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    app.InitializeDatabase();
}
#endregion

app.Run();