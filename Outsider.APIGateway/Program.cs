using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Outsider.APIGateway;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

    builder.Services.AddAuthentication()
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://localhost:44332";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
            };
        });
}
else
{
    builder.Configuration.AddJsonFile(Environment.GetEnvironmentVariable("ocelotProdPath")!, optional: false, reloadOnChange: true);

    builder.Services.AddAuthentication()
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://www.danieloliveira.net.br/Outsider.IdentityServer";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
            };
        });
}

builder.Services.AddOcelot();

var app = builder.Build();
app.UseMiddleware<CustomMiddleware>();
if(app.Environment.IsDevelopment())
{
    app.UseCors(cors =>
    {
        cors.AllowAnyHeader();
        cors.AllowAnyMethod();
        cors.AllowAnyOrigin();
    });
}

// Configure the HTTP request pipeline.
app.UseOcelot().Wait();
app.Run();
