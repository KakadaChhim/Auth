using Authentication.Configs;
using Authentication.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
