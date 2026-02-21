namespace ABC.BookStore.Depolar;
public class CreateDepoDtoValidator : AbstractValidator<CreateDepoDto>
{
    public CreateDepoDtoValidator(IStringLocalizer<BookStoreResource> localizer)
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