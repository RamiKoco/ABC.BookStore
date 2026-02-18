namespace ABC.BookStore.Ilceler;
public class UpdateIlceDtoValidator : AbstractValidator<UpdateIlceDto>
{
    public UpdateIlceDtoValidator(IStringLocalizer<BookStoreResource> localizer)
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

        RuleFor(x => x.IlId)
          .Must(x => x.HasValue && x.Value != Guid.Empty)
          .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
           localizer["City"]]);
    }
}
