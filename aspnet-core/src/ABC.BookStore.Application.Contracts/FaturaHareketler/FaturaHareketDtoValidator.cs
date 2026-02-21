namespace ABC.BookStore.FaturaHareketler;
public class FaturaHareketDtoValidator : AbstractValidator<FaturaHareketDto>
{
    public FaturaHareketDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Id"]]);

        RuleFor(x => x.HareketTuru)
            .IsInEnum()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["RowType"]])

            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["RowType"]]);

        RuleFor(x => x.StokId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Stok)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Stock"]]);

        RuleFor(x => x.HizmetId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Hizmet)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Service"]]);

        RuleFor(x => x.MasrafId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Masraf)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Expense"]]);

        RuleFor(x => x.DepoId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Stok)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Warehouse"]]);

        RuleFor(x => x.Miktar)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Quantity"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["Quantity"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BirimFiyat)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["UnitPrice"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["UnitPrice"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BrutTutar)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["GrossAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["GrossAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.IndirimTutar)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["DiscountAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["DiscountAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.KdvOrani)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["ValueAddedTaxRate"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["ValueAddedTaxRate"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.KdvHaricTutar)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["ExcludesValueAddedTaxAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["ExcludesValueAddedTaxAmount"], localizer["ToZero"],
             localizer["ThanZero"]]);

        RuleFor(x => x.KdvTutar)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["ValueAddedTaxAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["ValueAddedTaxAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.NetTutar)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["TotalAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["TotalAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}