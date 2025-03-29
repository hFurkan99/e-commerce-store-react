using App.API.Extensions;
using App.Application.Extensions;
using App.Persistence;
using App.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithFiltersExt()
    .AddSwaggerGenExt()
    .AddExceptionHandlerExt()
    .AddRepositoriesExt(builder.Configuration)
    .AddServicesExt();

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

await DbInitializer.InitDb(app);

app.Run();
