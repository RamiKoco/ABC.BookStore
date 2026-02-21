namespace ABC.BookStore.Faturalar;
public class SelectFaturaHareketDtoValidator : AbstractValidator<SelectFaturaHareketDto>
{
    public SelectFaturaHareketDtoValidator(IStringLocalizer localizer)
    {
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
             localizer["Point"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}