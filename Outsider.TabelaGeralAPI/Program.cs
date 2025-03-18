using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Outsider.TabelaGeralAPI.Config;
using Outsider.TabelaGeralAPI.Model.Context;
using Outsider.TabelaGeralAPI.RabbitMQSender;
using Outsider.TabelaGeralAPI.Repository;
using Outsider.TabelaGeralAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<OutsiderContext>(options =>
{
    var connection = builder.Configuration["OutsiderConnection"];
    options.UseSqlServer(connection);
});
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ITabelaGeralRepository, TabelaGeralRepository>();
builder.Services.AddScoped<ITabelaGeralItemRepository, TabelaGeralItemRepository>();

builder.Services.AddScoped<ITabelaGeralService, TabelaGeralService>();
builder.Services.AddScoped<ITabelaGeralItemService, TabelaGeralItemService>();

builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
builder.Services.AddControllers();


if (builder.Environment.IsDevelopment())
{

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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "Outsider");
    });

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Digite 'Bearer' [espaço] e seu token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
       },
        new List<string>()
    }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors(cors =>
    {
        cors.AllowAnyHeader();
        cors.AllowAnyMethod();
        cors.AllowAnyOrigin();
    });
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
