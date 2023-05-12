using DW.Api.Service;
using DW.Data.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using DW.Data.Database.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var name = Environment.GetEnvironmentVariable("DATABASENAME");
var user = Environment.GetEnvironmentVariable("DATABASEUSER");
var password = Environment.GetEnvironmentVariable("DATABASEPASSWORD");
var host = Environment.GetEnvironmentVariable("DATABASEHOST");
var port = Environment.GetEnvironmentVariable("DATABASEPORT");
var connectionString = $"User ID={user};Password={password};Host={host};Port={port};Database={name};";

builder.Services.AddDbContext<DwDbContext>(options => options.UseNpgsql(connectionString));

var tempbuilder = new DbContextOptionsBuilder<DwDbContext>();
tempbuilder.UseNpgsql(connectionString);
var tempdb = new DwDbContext(tempbuilder.Options);
tempdb.Database.Migrate();

builder.Services.Configure<JwtConfig>(config =>
{
    config.ExpiryTimeFrame = TimeSpan.Parse(Environment.GetEnvironmentVariable("EXPIRYTIMEFRAME"));
    config.Secret = Environment.GetEnvironmentVariable("SECRETJWT");
});

var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRETJWT"));
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    RequireExpirationTime = false,
    //ClockSkew = TimeSpan.Zero,
};
builder.Services.AddSingleton(tokenValidationParameters);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameters;
});

builder.Services.AddSwaggerGen(c =>
{
    //c.EnableAnnotations();
    c.UseInlineDefinitionsForEnums();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddDefaultIdentity<DwUser>(options=>options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DwDbContext>();


var app = builder.Build();
app.Urls.Add("http://0.0.0.0:35467");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
