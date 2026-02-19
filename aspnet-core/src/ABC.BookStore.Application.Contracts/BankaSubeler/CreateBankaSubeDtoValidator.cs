namespace ABC.BookStore.BankaSubeler;
public class CreateBankaSubeDtoValidator : AbstractValidator<CreateBankaSubeDto>
{
    public CreateBankaSubeDtoValidator(IStringLocalizer<BookStoreResource> localizer)
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

        RuleFor(x => x.BankaId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Bank"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}