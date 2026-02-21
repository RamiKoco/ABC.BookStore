namespace ABC.BookStore.Exceptions;
public class DuplicateCodeException : BusinessException
{
    public DuplicateCodeException(string kod) : base(BookStoreDomainErrorCodes.DuplicateKod)
    {
        WithData("kod", kod);
    }
}