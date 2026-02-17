using Volo.Abp;

namespace ABC.BookStore.Extensions;

public class CannotBeDeletedException : BusinessException
{
    public CannotBeDeletedException()
        : base("BookStore:CannotBeDeleted")
    {
    }
}