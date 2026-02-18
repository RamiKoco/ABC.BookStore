namespace ABC.BookStore.Exceptions;
public class ConnotBeDeletedException : BusinessException
{
    public ConnotBeDeletedException() : base(BookStoreDomainErrorCodes.ConnotBeDeleted)
    {
    }
}