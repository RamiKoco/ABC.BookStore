namespace ABC.BookStore.Stoklar;
public class CreateStokDtoValidator : AbstractValidator<CreateStokDto>
{
    public CreateStokDtoValidator(IStringLocalizer<BookStoreResource> localizer)
    {
        RuleFor(x => x.Kod)
           .NotEmpty()
           .WithMessage(localizer[BookStoreDomainErrorCodes.Required, localizer["Code"]])

           .MaximumLength(EntityConsts.MaxKodLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Code"],
            EntityConsts.MaxKodLength]);

        RuleFor(x => x.Ad)
            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required, localizer["Name"]])

            .MaximumLength(EntityConsts.MaxAdLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Name"],
             EntityConsts.MaxAdLength]);

        RuleFor(x => x.KdvOrani)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["ValueAddedTaxRate"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["ValueAddedTaxRate"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BirimFiyat)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["UnitPrice"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["UnitPrice"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.Barkod)
            .MaximumLength(EntityConsts.MaxBarkodLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["BarCode"], EntityConsts.MaxBarkodLength]);

        RuleFor(x => x.En)    
           .MaximumLength(EntityConsts.MaxAdLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Width"],
            EntityConsts.MaxAdLength]);

        RuleFor(x => x.Boy)
           .MaximumLength(EntityConsts.MaxAdLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Length"],
            EntityConsts.MaxAdLength]);
           
        RuleFor(x => x.Yukseklik)
           .MaximumLength(EntityConsts.MaxAdLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Height"],
            EntityConsts.MaxAdLength]);

        RuleFor(x => x.Alan)
           .MaximumLength(EntityConsts.MaxAdLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Area"],
            EntityConsts.MaxAdLength]);

        RuleFor(x => x.NetHacim)
           .MaximumLength(EntityConsts.MaxAdLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["ClearVolume"],
            EntityConsts.MaxAdLength]);

        RuleFor(x => x.BrutHacim)
           .MaximumLength(EntityConsts.MaxAdLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["GrossVolume"],
            EntityConsts.MaxAdLength]);

        RuleFor(x => x.NetAgirlik)
           .MaximumLength(EntityConsts.MaxAdLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["ClearWeight"],
            EntityConsts.MaxAdLength]);

        RuleFor(x => x.BrutAgirlik)
           .MaximumLength(EntityConsts.MaxAdLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["GrossWeight"],
            EntityConsts.MaxAdLength]);

        RuleFor(x => x.BirimId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Unit"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);

        RuleFor(x => x.Aciklama2)
          .MaximumLength(EntityConsts.MaxAciklamaLength)
          .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
           localizer["Description"], EntityConsts.MaxAciklamaLength]);

        RuleFor(x => x.Aciklama3)
          .MaximumLength(EntityConsts.MaxAciklamaLength)
          .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
           localizer["Description"], EntityConsts.MaxAciklamaLength]);

    }
}