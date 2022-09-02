using AutoMapper;
using BookMyShow.Configuration;
using BookMyShow.Extension;

var builder = WebApplication.CreateBuilder(args);

#region Configure and Register AutoMapper
var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);
#endregion


IConfiguration configuration = builder.Configuration;
builder.Services.RegisterSystemServices();
builder.Services.RegisterApplicationServices();

var app = builder.Build();


app.CreateMiddlewarePipeline();

app.Run();
