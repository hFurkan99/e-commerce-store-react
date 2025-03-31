using FluentValidation;

namespace App.Application.Features.Baskets.Add;

public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
{
    public AddItemRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Ürün Id bilgisi boş bırakılamaz.")
            .GreaterThan(0).WithMessage("Geçerli bir ürün Id'si giriniz.");

        RuleFor(x => x.BasketId).NotEmpty().WithMessage("Sepet Id bilgisi boş bırakılamaz.")
            .GreaterThan(0).WithMessage("Geçerli bir sepet Id'si giriniz.");

        RuleFor(x => x.Quantity).NotEmpty().WithMessage("Miktar bilgisi boş bırakılamaz.")
            .GreaterThan(0).WithMessage("Geçerli bir miktar giriniz.");
    }
}
