using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using App.Application.Features.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using App.Application.Features.Baskets;

namespace App.Application.Extensions;

public static class ApplicationExtensions
{
    private static readonly string[] configureOptions = ["tr-TR", "en-US"];

    public static IServiceCollection AddServicesExt(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddLocalization(options => options.ResourcesPath = "../App.Application/Resources");

       services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = configureOptions;
            options.DefaultRequestCulture = new RequestCulture("tr-TR");
            options.SupportedCultures = supportedCultures.Select(x => new CultureInfo(x)).ToList();
            options.SupportedUICultures = supportedCultures.Select(x => new CultureInfo(x)).ToList();
        });

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IBasketService, BasketService>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
