namespace ABC.BookStore.Makbuzlar;
public class UpdateMakbuzDtoValidator : AbstractValidator<UpdateMakbuzDto>
{
    public UpdateMakbuzDtoValidator(IStringLocalizer<BookStoreResource> localizer)
    {
        RuleFor(x => x.MakbuzNo)
            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["ReceiptNo"]])

            .MaximumLength(MakbuzConsts.MaxMakbuzNoLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["ReceiptNo"], MakbuzConsts.MaxMakbuzNoLength]);

        RuleFor(x => x.Tarih)
            .NotEmpty()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Date"]]);

        RuleFor(x => x.KasaId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.KasaIslem)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Cash"]]);

        RuleFor(x => x.KasaId)
            .Empty()
            .When(x => x.MakbuzTuru != MakbuzTuru.KasaIslem)
            .WithMessage(localizer[BookStoreDomainErrorCodes.IsNull,
             localizer["Cash"]]);

        RuleFor(x => x.CariId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.Tahsilat || x.MakbuzTuru == MakbuzTuru.Odeme)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["Customer"]]);

        RuleFor(x => x.CariId)
            .Empty()
            .When(x => x.MakbuzTuru != MakbuzTuru.Tahsilat && x.MakbuzTuru != MakbuzTuru.Odeme)
            .WithMessage(localizer[BookStoreDomainErrorCodes.IsNull,
             localizer["Customer"]]);

        RuleFor(x => x.BankaHesapId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.BankaIslem)
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["BankAccount"]]);

        RuleFor(x => x.BankaHesapId)
            .Empty()
            .When(x => x.MakbuzTuru != MakbuzTuru.BankaIslem)
            .WithMessage(localizer[BookStoreDomainErrorCodes.IsNull,
             localizer["BankAccount"]]);

        RuleFor(x => x.HareketSayisi)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["NumberOfTransactions"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["NumberOfTransactions"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.CekToplam)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["CheckTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["CheckTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.SenetToplam)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["BillOfExchangeTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["BillOfExchangeTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.PosToplam)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["PosTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["PosTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.NakitToplam)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["CashTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["CashTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BankaToplam)
            .NotNull()
            .WithMessage(localizer[BookStoreDomainErrorCodes.Required,
             localizer["BankTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[BookStoreDomainErrorCodes.GreaterThanOrEqual,
             localizer["BankTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[BookStoreDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);

        RuleForEach(x => x.MakbuzHareketler)
            .SetValidator(y => new MakbuzHareketDtoValidator(localizer));
    }
}