using Microsoft.Extensions.Options;

namespace App.API.Extensions
{
    public static class ConfigurePipelineExtensions
    {
        public static IApplicationBuilder UseConfigurePipelineExt(this WebApplication app)
        {
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseExceptionHandlerExt();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerExt();
            }

            app.UseHttpsRedirection();

            app.UseCors(option =>
            {
                option.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000");
            });

            app.UseAuthorization();

            return app;
        }
    }
}
