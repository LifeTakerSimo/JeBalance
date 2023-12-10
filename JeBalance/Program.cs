var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// need code to call the initialiser for the data base

app.MapGet("/", () => "Hello World!");

app.Run();

