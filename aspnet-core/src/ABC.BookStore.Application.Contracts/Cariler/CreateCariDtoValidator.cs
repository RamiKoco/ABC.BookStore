namespace ABC.BookStore.Cariler;
public class CreateCariDtoValidator:AbstractValidator<CreateCariDto>
{
    public CreateCariDtoValidator(IStringLocalizer<BookStoreResource> localizer)
    {
        RuleFor(x => x.Kod)
           .NotEmpty()
           .WithMessage(localizer[BookStoreDomainErrorCodes.Required, localizer["Code"]])

           .MaximumLength(EntityConsts.MaxKodLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Code"],
            EntityConsts.MaxKodLength]);

        RuleFor(x => x.Ad)     
            .MaximumLength(EntityConsts.MaxAdLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Name"],
             EntityConsts.MaxAdLength]);

        RuleFor(x => x.Soyad)
          .MaximumLength(EntityConsts.MaxAdLength)
          .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Name"],
           EntityConsts.MaxAdLength]);

        RuleFor(x => x.Unvan)
            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required, localizer["Title1"]])

            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Title1"],
             EntityConsts.MaxAciklamaLength]);       

        RuleFor(x => x.HesapTuru)
           .IsInEnum()
           .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
            localizer["AccountType"]])//Hesap Türü Alanı Zorunludur Msg.

           .NotEmpty()
           .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
            localizer["AccountType"]]);

        RuleFor(x => x.VergiDairesi)
            .MaximumLength(CariConsts.MaxVergiDairesiLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["TaxAdministration"], CariConsts.MaxVergiDairesiLength]);

        RuleFor(x => x.TCNo)
           .MaximumLength(EntityConsts.MaxTCNoLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
            localizer["IdNumber"], CariConsts.MaxTCNoLength]);

        RuleFor(x => x.VergiNo)
            .MaximumLength(CariConsts.MaxVergiNoLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["TaxNumber"], CariConsts.MaxVergiNoLength]);

        RuleFor(x => x.VDKodu)   
          .MaximumLength(EntityConsts.MaxKodLength)
          .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["TaxCode"],
           EntityConsts.MaxKodLength]);             

        RuleFor(x => x.Telefon)
            .MaximumLength(EntityConsts.MaxTelefonLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Telephone"], EntityConsts.MaxTelefonLength]);      

        RuleFor(x => x.Email)
            .MaximumLength(EntityConsts.MaxEmailLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Email"], EntityConsts.MaxEmailLength]);       

        RuleFor(x => x.Adres)
            .MaximumLength(EntityConsts.MaxAdresLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Address"], EntityConsts.MaxAdresLength]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
        
    }
}