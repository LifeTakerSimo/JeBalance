using Infrastructure;
using JeBalance;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddInfrastructure();

app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Redirect("/Interface/Pages/Index");
});

app.Run();

