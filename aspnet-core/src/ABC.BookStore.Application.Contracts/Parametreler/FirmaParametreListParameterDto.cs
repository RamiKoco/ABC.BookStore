namespace ABC.BookStore.Parametreler;
public class FirmaParametreListParameterDto : PagedResultRequestDto, IEntityDto
{
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}