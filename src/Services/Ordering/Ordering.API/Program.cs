var builder = WebApplication.CreateBuilder(args);

#region Add services to the container
var app = builder.Build();
#endregion

#region Configure the HTTP request pipeline
app.Run();
#endregion
