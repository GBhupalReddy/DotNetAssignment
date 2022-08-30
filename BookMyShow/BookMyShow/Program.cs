using AutoMapper;
using BookMyShow.Configuration;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Infrastructure.Repository.Dapper;
using BookMyShow.Infrastructure.Repository.EntityFramWork;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

#region Configure and Register AutoMapper
var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);
#endregion


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IDbConnection connection = new SqlConnection(@"Server= (localDb)\MSSQLLocalDB; DataBase=BookMyShow;Trusted_Connection=True;");
builder.Services.AddSingleton<IDbConnection>(connection);
builder.Services.AddTransient<IUserRepository, UserRepository>();

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
