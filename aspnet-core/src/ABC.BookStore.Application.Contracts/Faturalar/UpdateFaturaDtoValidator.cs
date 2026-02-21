namespace ABC.BookStore.Faturalar;
public class UpdateFaturaDtoValidator : AbstractValidator<UpdateFaturaDto>
{
    public UpdateFaturaDtoValidator(IStringLocalizer<BookStoreResource> localizer)
    {
        RuleFor(x => x.FaturaNo)
            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["InvoiceNumber"]])

            .MaximumLength(FaturaConsts.MaxFaturaNoLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["InvoiceNumber"], FaturaConsts.MaxFaturaNoLength]);

        RuleFor(x => x.Tarih)
            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Date"]]);

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

        RuleFor(x => x.HareketSayisi)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["NumberOfTransactions"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["NumberOfTransactions"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.CariId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Customer"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);

        RuleForEach(x => x.FaturaHareketler)
            .SetValidator(y => new FaturaHareketDtoValidator(localizer));
    }
}