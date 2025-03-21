using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Outsider.PedidoAPI;
using Outsider.PedidoAPI.Config;
using Outsider.PedidoAPI.MessageConsumer;
using Outsider.PedidoAPI.Model.Context;
using Outsider.PedidoAPI.RabbitMQSender;
using Outsider.PedidoAPI.Repository;
using Outsider.PedidoAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration["OutsiderConnection"];
builder.Services.AddDbContext<OutsiderContext>(options =>
{
    options.UseSqlServer(connection);
});

var dbContextBuilder = new DbContextOptionsBuilder<OutsiderContext>();
dbContextBuilder.UseSqlServer(connection);


IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddSingleton(new PedidoRepository(dbContextBuilder.Options, mapper));
builder.Services.AddSingleton(new ProdutoRepository(dbContextBuilder.Options));

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();
builder.Services.AddHostedService<RabbitMQPagamentoConsumer>();

builder.Services.AddSingleton(new ProdutoRepository(dbContextBuilder.Options));
builder.Services.AddHostedService<RabbitMQProdutoConsumer>();

builder.Services.AddSingleton(new TabelaGeralItemRepository(dbContextBuilder.Options));
builder.Services.AddHostedService<RabbitMQTabelaGeralConsumer>();
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
        Description = @"Digite 'Bearer' [espa�o] e seu token",
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

app.UseMiddleware<CustomMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();


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
