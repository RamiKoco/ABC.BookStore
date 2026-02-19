namespace ABC.BookStore.Bankalar;
public class BankaListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}