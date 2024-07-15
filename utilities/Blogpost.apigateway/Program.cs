using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
             builder => builder.WithOrigins("http://localhost:4200")
                               .AllowCredentials()
                               .AllowAnyMethod()
                               .AllowAnyHeader());
});
var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();
