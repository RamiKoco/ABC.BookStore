using Volo.Abp;

namespace ABC.BookStore.Extensions;

public class DuplicateCodeException : BusinessException
{
    public DuplicateCodeException(string kod)
        : base("BookStore:DuplicateCode")
    {
        WithData("kod", kod);
    }
}