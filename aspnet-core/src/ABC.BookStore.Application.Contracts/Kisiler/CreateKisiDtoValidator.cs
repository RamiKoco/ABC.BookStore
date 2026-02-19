namespace ABC.BookStore.Kisiler;
public class CreateKisiDtoValidator : AbstractValidator<CreateKisiDto>
{
    public CreateKisiDtoValidator(IStringLocalizer<BookStoreResource> localizer)
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

        RuleFor(x => x.Soyad)
            .MaximumLength(EntityConsts.MaxAdLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["Surname"],
             EntityConsts.MaxAdLength]);

        RuleFor(x => x.TCNo)
          .MaximumLength(EntityConsts.MaxTCNoLength)
          .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
           localizer["IdNumber"], EntityConsts.MaxTCNoLength]);
        

        RuleFor(x => x.Telefon)
            .MaximumLength(EntityConsts.MaxTelefonLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Telephone"], EntityConsts.MaxTelefonLength]);      

        RuleFor(x => x.Email)
           .MaximumLength(EntityConsts.MaxEmailLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
            localizer["Email"], EntityConsts.MaxEmailLength]);

        RuleFor(x => x.DogumYeri)
          .MaximumLength(EntityConsts.MaxAdLength)
          .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght, localizer["BirthPlace"],
           EntityConsts.MaxAdLength]);       

         RuleFor(x => x.MedeniHal)
          .IsInEnum()          
          .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
           localizer["CivilStatus"]]);

        RuleFor(x => x.Cinsiyet)
          .IsInEnum()
          .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
           localizer["Gender"]])//Hesap Türü Alanı Zorunludur Msg.
          .NotEmpty()
          .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
           localizer["Gender"]]);

        RuleFor(x => x.KanGrubu)
          .IsInEnum()
          .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
           localizer["BloodGroup"]])//Hesap Türü Alanı Zorunludur Msg.
          .NotEmpty()
          .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
           localizer["BloodGroup"]]);       

        RuleFor(x => x.Image)
            .MaximumLength(EntityConsts.MaxImageLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Image"], EntityConsts.MaxImageLength]);    

        RuleFor(x => x.Aciklama)
          .MaximumLength(EntityConsts.MaxAciklamaLength)
          .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
           localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}