namespace ABC.BookStore.CariSubeler;
public class SelectCariSubeDtoValidator : AbstractValidator<SelectCariSubeDto>
{
    public SelectCariSubeDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Aciklama)
           .MaximumLength(EntityConsts.MaxAciklamaLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
            localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}
