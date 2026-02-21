namespace ABC.BookStore.Cariler;
public class CariListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public CariHesapTuru? HesapTuru { get; set; }
    public Guid SubeId { get; set; }
    public bool Durum { get; set; }
    public override int MaxResultCount { get; set; } = MaxMaxResultCount = 5000;
}