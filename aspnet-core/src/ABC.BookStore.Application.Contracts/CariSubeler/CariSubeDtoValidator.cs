namespace ABC.BookStore.CariSubeler;
public class CariSubeDtoValidator : AbstractValidator<CariSubeDto>
{
    public CariSubeDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Id"]]);

        RuleFor(x => x.Aciklama)
           .MaximumLength(EntityConsts.MaxAciklamaLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
            localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}
