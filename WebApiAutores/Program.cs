using WebApiAutores;

var builder = WebApplication.CreateBuilder(args);

Startup startup = new Startup(builder.Configuration);

startup.ConfigureService(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
