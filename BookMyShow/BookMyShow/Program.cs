using AutoMapper;
using BookMyShow.Configuration;
using BookMyShow.Extension;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Configure and Register AutoMapper
var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);
#endregion

Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));


IConfiguration configuration = builder.Configuration;
builder.Services.RegisterSystemServices(configuration);
builder.Services.RegisterApplicationServices(configuration);



var app = builder.Build();


app.CreateMiddlewarePipeline();

app.Run();
