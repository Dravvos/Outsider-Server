using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Outsider.AvaliacaoAPI.Config;
using Outsider.AvaliacaoAPI.MessageConsumer;
using Outsider.AvaliacaoAPI.Model.Context;
using Outsider.AvaliacaoAPI.MongoDB;
using Outsider.AvaliacaoAPI.Repository;
using Outsider.AvaliacaoAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration["OutsiderConnection"];
builder.Services.AddDbContext<OutsiderContext>(options =>
{    
    options.UseSqlServer(connection);
});

builder.Services.AddSingleton<MongoDbContext>();


IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<SyncService>();
builder.Services.AddScoped<IAvaliacaoSqlRepository, AvaliacaoSqlRepository>();
builder.Services.AddScoped<IAvaliacaoMongoRepository, AvaliacaoMongoRepository>();

builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();


var dbContextBuilder = new DbContextOptionsBuilder<OutsiderContext>();
dbContextBuilder.UseSqlServer(connection);

builder.Services.AddSingleton(new TabelaGeralItemRepository(dbContextBuilder.Options));
builder.Services.AddSingleton(new ProdutoRepository(dbContextBuilder.Options));

builder.Services.AddHostedService<RabbitMQProdutoConsumer>();
builder.Services.AddHostedService<RabbitMQTabelaGeralConsumer>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = "https://localhost:44332";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
    };
});

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
}


app.UseCors(cors =>
{
    cors.AllowAnyHeader();
    cors.AllowAnyMethod();
    cors.AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
