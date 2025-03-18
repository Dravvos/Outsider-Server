using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Outsider.CarrinhoAPI.Model.Context;
using Outsider.CarrinhoAPI.Config;
using Outsider.CarrinhoAPI.RabbitMQSender;
using Outsider.CarrinhoAPI.Repository;
using StackExchange.Redis;
using Outsider.CarrinhoAPI.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Outsider.CarrinhoAPI.MessageConsumer;
using Outsider.CarrinhoAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration["OutsiderConnection"];
builder.Services.AddDbContext<OutsiderContext>(options =>
{    
    options.UseSqlServer(connection);
});

var redis = ConnectionMultiplexer.Connect(builder.Configuration["RedisConnection"]!);
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
builder.Services.AddScoped<ICupomRepository, CupomRepository>();

builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();
builder.Services.AddScoped<ICupomService, CupomService>();

builder.Services.AddHttpClient<ICupomRepository, CupomRepository>(s=>s.BaseAddress=new Uri(builder.Configuration["CupomAPI"]!));

builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();

var dbContextBuilder = new DbContextOptionsBuilder<OutsiderContext>();
dbContextBuilder.UseSqlServer(connection);

builder.Services.AddSingleton(new TabelaGeralItemRepository(dbContextBuilder.Options));
builder.Services.AddSingleton(new ProdutoRepository(dbContextBuilder.Options));

builder.Services.AddHostedService<RabbitMQProdutoConsumer>();
builder.Services.AddHostedService<RabbitMQTabelaGeralConsumer>();

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
}
app.UseMiddleware<CustomMiddleware>();
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
