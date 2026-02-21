namespace ABC.BookStore.MakbuzHareketler;
public class MakbuzHareketDtoValidator : AbstractValidator<MakbuzHareketDto>
{
    public MakbuzHareketDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Id"]]);

        RuleFor(x => x.OdemeTuru)
            .IsInEnum()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["PaymentType"]])

            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["PaymentType"]]);

        RuleFor(x => x.TakipNo)
            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["TrackingNo"]])

            .MaximumLength(MakbuzHareketConsts.MaxTakipNoLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["TrackingNo"], MakbuzHareketConsts.MaxTakipNoLength]);

        RuleFor(x => x.CekBankaId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Bank"]]);

        RuleFor(x => x.CekBankaId)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek)
            .WithMessage(localizer[BookStoreDomainErrorCodes.IsNull,
             localizer["Bank"]]);

        RuleFor(x => x.CekBankaSubeId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["BankBranch"]]);

        RuleFor(x => x.CekBankaId)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek)
            .WithMessage(localizer[BookStoreDomainErrorCodes.IsNull,
             localizer["BankBranch"]]);

        RuleFor(x => x.CekHesapNo)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["CheckAccountNo"]])

            .MaximumLength(MakbuzHareketConsts.MaxCekHesapNoLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["CheckAccountNo"], MakbuzHareketConsts.MaxCekHesapNoLength]);

        RuleFor(x => x.CekHesapNo)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek)
            .WithMessage(localizer[BookStoreDomainErrorCodes.IsNull,
             localizer["CheckAccountNo"]]);

        RuleFor(x => x.BelgeNo)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Cek)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["CheckNo"]])

            .MaximumLength(MakbuzHareketConsts.MaxBelgeNoLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["CheckNo"], MakbuzHareketConsts.MaxBelgeNoLength]);

        RuleFor(x => x.Vade)
           .NotEmpty()
           .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
            localizer["Date"]]);

        RuleFor(x => x.AsilBorclu)
           .NotEmpty()
           .When(x => x.OdemeTuru == OdemeTuru.Cek || x.OdemeTuru == OdemeTuru.Senet)
           .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
            localizer["PrincipalDebtor"]])

           .MaximumLength(MakbuzHareketConsts.MaxAsilBorcluLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
            localizer["PrincipalDebtor"], MakbuzHareketConsts.MaxAsilBorcluLength]);

        RuleFor(x => x.AsilBorclu)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek && x.OdemeTuru != OdemeTuru.Senet)
            .WithMessage(localizer[BookStoreDomainErrorCodes.IsNull,
             localizer["PrincipalDebtor"]]);

        RuleFor(x => x.Ciranta)
           .MaximumLength(MakbuzHareketConsts.MaxCirantaLength)
           .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
            localizer["Endorser"], MakbuzHareketConsts.MaxCirantaLength]);

        RuleFor(x => x.Ciranta)
            .Empty()
            .When(x => x.OdemeTuru != OdemeTuru.Cek && x.OdemeTuru != OdemeTuru.Senet)
            .WithMessage(localizer[BookStoreDomainErrorCodes.IsNull,
             localizer["Endorser"]]);

        RuleFor(x => x.KasaId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Nakit)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["CashAccount"]]);

        RuleFor(x => x.BankaHesapId)
            .NotEmpty()
            .When(x => x.OdemeTuru == OdemeTuru.Banka|| x.OdemeTuru == OdemeTuru.Pos)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["BankAccount"]]);

        RuleFor(x => x.Tutar)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Amount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["Amount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BelgeDurumu)
            .IsInEnum()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["MeansOfPaymentState"]])

            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["MeansOfPaymentState"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}