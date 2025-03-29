using FluentValidation;
using Microsoft.Extensions.Localization;

namespace App.Application.Features.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(IStringLocalizer<CreateProductRequestValidator> localizer)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(localizer["ProductNameRequired"])
            .MaximumLength(100).WithMessage("Ürün ismi en fazla 100 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(localizer["ProductDescriptionRequired"])
            .MaximumLength(500).WithMessage("Ürün açıklaması en fazla 500 karakter olabilir.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

        RuleFor(x => x.PictureUrl)
            .NotEmpty().WithMessage("Ürün resmi URL'si boş bırakılamaz.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Ürün türü seçilmelidir.");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Ürün markası boş bırakılamaz.")
            .MaximumLength(50).WithMessage("Ürün markası en fazla 50 karakter olabilir.");

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");
    }
}
