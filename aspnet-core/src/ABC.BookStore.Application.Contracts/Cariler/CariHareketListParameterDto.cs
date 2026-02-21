namespace ABC.BookStore.Cariler;
public class CariHareketListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
    public Guid CariId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}