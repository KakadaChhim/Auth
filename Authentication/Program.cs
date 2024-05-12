using System.Text;
using Authentication.Configs;
using Authentication.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer" , new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer [jwt]'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    options.AddSecurityRequirement(new OpenApiSecurityRequirement{{scheme, Array.Empty<string>()}});
});

var secret = builder.Configuration.GetSection("JWT").GetSection("Secret").Value 
             ?? throw new InvalidOperationException("Secret not Found!"); 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});

//Add Automapper
var confg = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfig());
});
// register DbContext
builder.Services.AddDbContext<Authentication.DbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var mapper = confg.CreateMapper();
builder.Services.AddSingleton(mapper);
// lower case url 
builder.Services.AddRouting(option => option.LowercaseUrls = true);


// Add logic service
builder.AddLogics();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
