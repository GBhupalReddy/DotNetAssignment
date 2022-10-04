using BookMyShow.Middleware;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using System.Text;

namespace BookMyShow.Extension
{
    public static class WebAppExtension
    {
        public static void CreateMiddlewarePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                app.UseSwagger();
                // app.UseSwaggerUI();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //Register Exception handler Middleware
            app.UseCors("MyPolicy");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
        }
        
    }
}
