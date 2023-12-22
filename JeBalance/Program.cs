var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Redirect("/Interface/Pages/Index");
});

app.Run();

// need code to call the initialiser for the data base