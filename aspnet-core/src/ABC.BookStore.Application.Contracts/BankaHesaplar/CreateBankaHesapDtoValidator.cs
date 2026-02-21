namespace ABC.BookStore.BankaHesaplar;
public class CreateBankaHesapDtoValidator : AbstractValidator<CreateBankaHesapDto>
{    
    public CreateBankaHesapDtoValidator(IStringLocalizer<BookStoreResource> localizer)
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

        RuleFor(x => x.HesapTuru)
            .IsInEnum()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["AccountType"]])//Hesap Türü Alanı Zorunludur Msg.

            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["AccountType"]]);       

        RuleFor(x => x.DovizT)
         .IsInEnum()
         .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
          localizer["ExchangeRate"]]);

        RuleFor(x => x.BankaSubeId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["BankBranch"]]);

        RuleFor(x => x.CariId)
        .Must(x => x.HasValue && x.Value != Guid.Empty)
        .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
         localizer["Customer"]]);

        RuleFor(x => x.HesapNo)
            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["AccountNumber"]])

            .MaximumLength(BankaHesapConsts.MaxHesapNoLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["AccountNumber"], BankaHesapConsts.MaxHesapNoLength]);

        RuleFor(x => x.IbanNo)
            .MaximumLength(BankaHesapConsts.MaxIbanNoLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Iban"], BankaHesapConsts.MaxIbanNoLength]);

        RuleFor(x => x.SubeId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Branch"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}